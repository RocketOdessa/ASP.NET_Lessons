using LessonFirst_MVCBasic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LessonFirst_MVCBasic.Controllers
{
    public class HomeController : Controller
    {
        static List<Music> Musics = new List<Music>()
        {
            new Music {id = 1, AlbumName = "Expactions",Singer = "Bebe Rexha" , Songs = new List<string>
            {
                "Ferrari",
                "I'm a mess",
                "Shining Stars",
                "Knees"
            }},
            new Music {id = 2, AlbumName = "Kamikaze", Singer = "Eminem", Songs = new List<string>
            {
                "The ringer",
                "Greatest",
                "Lucky You",
                "Venom"
            }},
            new Music {id = 3, AlbumName = "The beutiful and damned", Singer = "G_Eazy", Songs = new List<string>
            {
                "No Limit",
                "Him & I",
                "Eazy",
                "Love is gone"
            }}
        };

        public ActionResult Index()
        {
            ViewBag.TestMusic = Musics;
            return View();
        }

        [HttpGet]
        public ActionResult Buy (int id)
        {
            ViewBag.ID = id;

            return View();
        }

        [HttpPost]
        public string Buy()
        {
            return "Thx for took this album";
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Music musics)
        {
            Musics.Add(new Music() {
                id = musics.id,
                AlbumName = musics.AlbumName,
                Singer = musics.Singer,
                Songs = musics.Songs
            });

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Music m = Musics.Find(i => i.id == id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Music m = Musics.Find(i => i.id == id);
            if (m == null)
            {
                return HttpNotFound();
            }
            Musics.Remove(m);
            return RedirectToAction("Index");
        }
    }
}