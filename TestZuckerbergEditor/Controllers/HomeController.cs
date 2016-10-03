using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestZuckerbergEditor.Models;
using System.Data.Entity;
using System.IO;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;




namespace TestZuckerbergEditor.Controllers
{

    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Home()
        {
            ViewModelForHomeView viewModelForHomeView = new ViewModelForHomeView();
            List<ViewModelForHomeView> listOfLinks = new List<ViewModelForHomeView>();

            using (WebsiteContext websiteContext = new WebsiteContext())
            {
                // websiteContext.Database.Initialize(true); // initierer db uten å vente på at den brukes
                string wallOwner = User.Identity.GetUserId();
                var allWalls = websiteContext.Walls.Where(x => x.wallOwner == wallOwner).ToList();
                foreach (Wall wall in allWalls)
                {
                    listOfLinks.Add(new ViewModelForHomeView
                    {
                        description = wall.wallName,
                        posterIdentifier = wall.uniqueIdentifier
                    });

                }
            }
            return View(listOfLinks);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddWall()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddWall(string wallName)
        {
            return RedirectToAction("CreatePoster", new { wallName = wallName });
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreatePoster(string wallName)
        {
            ViewBag.wallName = wallName;
            ViewBag.SideName = "Page 1";
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreatePoster(string button, string title, HttpPostedFileBase file, string question, string wallName, string uniqueIdentifier)
        {
            if ((question == "") || (question == null)) question = "";
            if ((title == "") || (title == null)) title = "";
            string imgName_temp = "";
            if( (wallName == "") )
            {
                wallName = "NoName";
            }

            if (button == "AddMorePosters")
            {
                //add the wall to the database if it is not created already
                using (WebsiteContext websiteContext = new WebsiteContext())
                {
                    Wall wall = null;
                    if (websiteContext.Walls.Where(x => x.uniqueIdentifier == uniqueIdentifier).Count() != 0)
                    {
                        wall = websiteContext.Walls.Where(x => x.uniqueIdentifier == uniqueIdentifier).First();
                    }
                    string posterPageNr = "1";
                    if (wall != null)// wall has been created check for posters and add new poster
                    {
                        //Eager loading
                        wall = websiteContext.Walls.Where(x => x.uniqueIdentifier == uniqueIdentifier).Include("Posters").First();
                        int posterCount = websiteContext.Walls.Where(t => t.uniqueIdentifier == uniqueIdentifier).SelectMany(o => o.posters).Count();
                        posterPageNr = (posterCount + 1).ToString();
                        ViewBag.SideName = "Page " + (posterCount + 2).ToString(); ;
                        string nextPageLink = (posterCount + 2).ToString();                    
                        if (file != null && file.ContentLength > 0) imgName_temp = "file" + posterPageNr + Path.GetExtension(file.FileName);
                        wall.posters.Add(new Poster() { nextPageLink = nextPageLink, title = title, imageName = imgName_temp, question = question, posterPageNr = posterPageNr });
                        websiteContext.SaveChanges();
                    }
                    else //wall does not exist - create new wall and add poster
                    {
                        uniqueIdentifier = Guid.NewGuid().ToString();
                        string nextPageLink = "2";
                        ViewBag.SideName = "Page " + nextPageLink;
                        string wallOwner = User.Identity.GetUserId();                     
                        if (file != null && file.ContentLength > 0) imgName_temp = "file" + posterPageNr + Path.GetExtension(file.FileName);
                        Wall newWall = new Wall() { wallName = wallName, uniqueIdentifier = uniqueIdentifier, wallOwner = wallOwner, posters = new List<Poster>() { new Poster() { nextPageLink = nextPageLink, title = title, imageName = imgName_temp, question = question, posterPageNr = posterPageNr } } };
                        websiteContext.Walls.Add(newWall);
                        websiteContext.SaveChanges();
                    }

                    if (file != null && file.ContentLength > 0) // A file is found and we try to save it
                    {                        
                        string path = Server.MapPath("~/Images").ToString() + @"\" + uniqueIdentifier;
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        var savePath = Path.Combine(path, imgName_temp);
                        file.SaveAs(savePath); // saving the file - add logic and error checking
                    }

                    ViewBag.uniqueIdentifier = uniqueIdentifier;
                    return View();
                }

            }

            if (button == "ThisIsMyLastPoster")
            {
                //add the wall to the database if it is not created already
                using (WebsiteContext websiteContext = new WebsiteContext())
                {
                    Wall wall = null;
                    if (websiteContext.Walls.Where(x => x.uniqueIdentifier == uniqueIdentifier).Count() != 0)
                    {
                        wall = websiteContext.Walls.Where(x => x.uniqueIdentifier == uniqueIdentifier).First();
                    }
                    string posterPageNr = "1";
                    if (wall != null)// wall has been created check for posters and add new poster
                    {
                        //Eager loading
                        wall = websiteContext.Walls.Where(x => x.uniqueIdentifier == uniqueIdentifier).Include("Posters").First();
                        int posterCount = websiteContext.Walls.Where(t => t.uniqueIdentifier == uniqueIdentifier).SelectMany(o => o.posters).Count();
                        posterPageNr = (posterCount + 1).ToString();
                        string nextPageLink = "1";                      
                        if (file != null && file.ContentLength > 0) imgName_temp = "file" + posterPageNr + Path.GetExtension(file.FileName);
                         wall.posters.Add(new Poster() { nextPageLink = nextPageLink, title = title, imageName = imgName_temp, question = question, posterPageNr = posterPageNr });
                        websiteContext.SaveChanges();
                    }
                    else //wall does not exist - create new wall and add poster
                    {
                        uniqueIdentifier = Guid.NewGuid().ToString();
                        string nextPageLink = "1";
                        string wallOwner = User.Identity.GetUserId();                     
                        if (file != null && file.ContentLength > 0) imgName_temp = "file" + posterPageNr + Path.GetExtension(file.FileName);
                        Wall newWall = new Wall() { wallName = wallName, uniqueIdentifier = uniqueIdentifier, wallOwner = wallOwner, posters = new List<Poster>() { new Poster() { nextPageLink = nextPageLink, title = title, imageName = imgName_temp, question = question, posterPageNr = posterPageNr } } };
                        //Wall newWall = new Wall() { wallName = wallName, uniqueIdentifier = uniqueIdentifier, posters = new List<Poster>() { new Poster() { nextPageLink = nextPageLink, title = title, imageName = file.FileName, question = question, posterPageNr = posterPageNr } } };
                        websiteContext.Walls.Add(newWall);
                        websiteContext.SaveChanges();
                    }

                    if (file != null && file.ContentLength > 0) // A file is found and we try to save it
                    {                      
                        string path = Server.MapPath("~/Images").ToString() + @"\" + uniqueIdentifier;
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        var savePath = Path.Combine(path, imgName_temp);
                        file.SaveAs(savePath); // saving the file - add logic and error checking
                    }


                    ViewBag.uniqueIdentifier = uniqueIdentifier;
                    return RedirectToAction("Home", "home");
                }
            }
            return View();
        }

        [Authorize(Users = "johansen_oyvind@hotmail.com,test2@home.no")]
        [HttpGet]
        public ActionResult getUsers()
        {
            List<String> listOfNames = null;
            using (ApplicationDbContext websiteUsers = new ApplicationDbContext())
            {
                listOfNames = (from u in websiteUsers.Users
                           select u.UserName).ToList<String>();               
            }
            return View(listOfNames);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult MySite(string uniqueIdentifier, string posterPageNr)
        {
            using (WebsiteContext websiteContext = new WebsiteContext())
            {
                ViewModelClass viewModelClass = new ViewModelClass();

                //Explicit Loading
                var wall = websiteContext.Walls.Where(x => x.uniqueIdentifier == uniqueIdentifier).First();
                websiteContext.Entry(wall)
                                .Collection(c => c.posters)
                                .Query()
                                .Where(h => h.posterPageNr == posterPageNr)
                                .Include(x => x.comments)
                                .Load();

                // LazyLoad (laster inn alt fr db utfører query i minnet?)
                //websiteContext.Configuration.LazyLoadingEnabled = true;
                //var dep = websiteContext.Walls.Where(X => X.uniqueIdentifier == uniqueIdentifier).First();
                //var d = dep.posters.Where(x => x.posterPageNr == posterPageNr).First();
                //var c = d.comments.ToList();
                //websiteContext.Configuration.LazyLoadingEnabled = false;             
                viewModelClass.uniqueIdentifier = wall.uniqueIdentifier;
                viewModelClass.title = wall.posters[0].title;
                if(wall.posters[0].imageName != "")
                {
                    viewModelClass.imageName = @"~/images/" + wall.uniqueIdentifier + @"/" + wall.posters[0].imageName;
                }
                else
                {
                    viewModelClass.imageName = "";
                }
                
                viewModelClass.question = wall.posters[0].question;
                viewModelClass.nextPageLink = wall.posters[0].nextPageLink;


                List<string> myList = new List<string>();
                foreach (Comment comment in wall.posters[0].comments)
                {
                    myList.Add(comment.comment);
                }
                viewModelClass.comments = myList;
                return View(viewModelClass);
            }

            // return View("~/Views/Shared/Error.cshtml");
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Mysite")]
        public ActionResult MySite_post(string uniqueIdentifier, string posterPageNr, string inputComment)
        {
            using (WebsiteContext websiteContext = new WebsiteContext())
            {

                //Explicit Loading
                var wall = websiteContext.Walls.Where(x => x.uniqueIdentifier == uniqueIdentifier).First();
                websiteContext.Entry(wall)
                              .Collection(c => c.posters)
                              .Query()
                              .Where(h => h.posterPageNr == posterPageNr)
                              .Include(x => x.comments)
                              .Load();

                Comment newComment = new Comment();
                wall.posters[0].comments.Add(new Comment { comment = inputComment });
                websiteContext.SaveChanges();

                return RedirectToAction("MySite", new { uniqueIdentifier = uniqueIdentifier, posterPageNr = posterPageNr });
            }

        }

    }
}

