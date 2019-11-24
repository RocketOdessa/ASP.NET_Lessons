using GemBox.Spreadsheet;
using Product.Domain.Abstract;
using Product.Domain.Concrete;
using StockApp_Lesson_Four.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StockApp_Lesson_Four.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository repository;
        public int pageSize = 3;
        // GET: Product
        public ProductController(IProductRepository repoParam)
        {
            repository = repoParam;
        }

        public ActionResult Index(string searchString, int page = 1)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return View(new ProductListViewModel
                {
                    Products = repository.Products.Where(product => product.ProductName.Contains(searchString)).OrderBy(p => p.ID).Skip((page - 1) * pageSize).Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = searchString == null ? repository.Products.Count() : repository.Products.Where(product => product.ProductName.Contains(searchString)).Count()
                    }
                });
            }
            else
            {
                return View(new ProductListViewModel
                {
                    Products = repository.Products.OrderBy(p => p.ID).Skip((page - 1) * pageSize).Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = repository.Products.Count()
                    }
                });
            }
        }

        [Route("Json", Name = "Json")]
        public JsonResult JsonInfo()
        {
            return Json(repository.Products, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OrderByPrice(string sortOrder, int page = 1)
        {
            ViewBag.UANSortParam = sortOrder == "UAN" ? "UAND" : "UAN";
            ViewBag.USDSortParam = sortOrder == "USD" ? "USDD" : "USD";


            //Take from memory
            var products = from prod in repository.Products select prod;
            switch (sortOrder)
            {
                case "UAND":
                    return View(new ProductListViewModel
                    {
                        //Take from memory
                        Products = repository.Products.OrderByDescending(p => p.PriceUAN).Skip((page - 1) * pageSize).Take(pageSize),
                        PagingInfo = new PagingInfo
                        {
                            CurrentPage = page,
                            ItemsPerPage = pageSize,
                            TotalItems = repository.Products.Count()
                        }
                    });
                case "USDD":
                    return View(new ProductListViewModel
                    {
                        //Take from memory
                        Products = repository.Products.OrderByDescending(p => p.PriceUSD).Skip((page - 1) * pageSize).Take(pageSize),
                        PagingInfo = new PagingInfo
                        {
                            CurrentPage = page,
                            ItemsPerPage = pageSize,
                            TotalItems = repository.Products.Count()
                        }
                    });
                default:
                    return View(new ProductListViewModel
                    {
                        //Take from memory                
                        Products = repository.Products.OrderBy(p => p.PriceUAN).Skip((page - 1) * pageSize).Take(pageSize),
                        PagingInfo = new PagingInfo
                        {
                            CurrentPage = page,
                            ItemsPerPage = pageSize,
                            TotalItems = repository.Products.Count()
                        }
                    });
            }
        }
        public ActionResult DownloadFile()
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            // Create a new empty spreadsheet.

            var workbook = new ExcelFile();
            var dataTable = new DataTable();

            using (var context = new EFProductContext())
            {
                var conn = context.Database.Connection;
                var connectionState = conn.State;

                try
                {
                    if (connectionState != ConnectionState.Open) conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "select * from Products";
                        using (var reader = cmd.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            var worksheet = workbook.Worksheets.Add("DataTable to Sheet");
            // Insert DataTable to an Excel worksheet.
            worksheet.InsertDataTable(dataTable,
                new InsertDataTableOptions()
                {
                    ColumnHeaders = true,
                    StartRow = 2
                });
            // Add spreadsheet content.
            workbook.Worksheets.Add("Набор Товаров").Cells[0, 0].Value = "Все наши товары!";

            byte[] fileContents;

            var options = SaveOptions.XlsxDefault;

            // Save spreadsheet to XLSX format in byte array.
            using (var stream = new MemoryStream())
            {
                workbook.Save(stream, options);

                fileContents = stream.ToArray();
            }

            // Stream spreadsheet to browser in XLSX format.
            return File(fileContents, options.ContentType, "Stock.xlsx");
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        //Second variant of generation Export File using low level response
        public ActionResult ExportToExcel()
        {
            var gv = new GridView();
            gv.DataSource = ToDataTable<Product.Domain.Product>(repository.Products.ToList());
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xlsx");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);

            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();

            return View("Index");
        }
    }
}