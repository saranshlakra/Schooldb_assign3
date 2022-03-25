using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Schooldb_assign3.Models;

namespace Schooldb_assign3.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        //Get: Teachers List
        [Route("teacher/list/{teacherInfo}")]
        public ActionResult List( string teacherInfo)
            // List(....) name that we give in our form
        {
            //get teahcer
            //connect to db layer
            //return the teachers to the view Teacher List.csHtml
            // controller we can change it to any name
            teacherdataController controller = new teacherdataController();
            IEnumerable<teachers> Teachers = controller.ListTeachers(teacherInfo);

            return View(Teachers); 

        }

        // get: teachers/Show/{teacherid}
       // [Route("teachers/show/{teacherid}")]
        public ActionResult show(int id)
        {
            // these two lines, this is how we get selected teacher info from find teachers with the help of teacherid
            teacherdataController controller = new teacherdataController();
            // data type 
            teachers selectedteacher = controller.Findteacher(id);

            return View(selectedteacher);
        }
    }
}