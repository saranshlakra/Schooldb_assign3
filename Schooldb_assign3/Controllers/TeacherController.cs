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
        // this will return the view and supporting get request
        // GET: /Teacher/Add
        public ActionResult Add()
        {
            return View(); 
        }

        //POST: /Teacher/Addnew
        [HttpPost]
        public ActionResult Addnew(string teachersFName, string teachersLName, string salary, string hireDate)
        {

            teachers NewTeacher = new teachers();
            NewTeacher.teacherfname = teachersFName;
            NewTeacher.teacherlname = teachersLName;
            NewTeacher.salary = salary;
            NewTeacher.hiredate = hireDate;



            teacherdataController controller = new teacherdataController();
            controller.AddTeacher(NewTeacher);

              if (NewTeacher.teacherfname != "")
              {
                 return RedirectToAction("List"); // redirect to list view}
             }

             return View();
           // return RedirectToAction("List"); // redirect to list view}

        }


        // get: teachers/deleteconfirm/{teacherid}
        // [Route("teachers/deleteconfirm/{teacherid}")]
        public ActionResult deleteconfirm(int id)
        {
            // these two lines, this is how we get selected teacher info from find teachers with the help of teacherid
            teacherdataController controller = new teacherdataController();
            // data type 
            teachers selectedteacher = controller.Findteacher(id);

            return View(selectedteacher);
        }


        //post request to delete the teacher
        //Post: /teacher/delete/{id}
        [HttpPost]
        public ActionResult delete(int id)
        {
            teacherdataController controller = new teacherdataController();
            controller.DeleteTeacher(id); // ti ll find the teacher delete method
            return RedirectToAction("List"); // it will return the updated teachers list
        }

       //GET: /teachers/Edit/{id}
       /// <summary>
       /// this will return the webpage of the teacher
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public ActionResult Edit(int id)
        {
            // Passing info to the view so that we can show

            teacherdataController controller = new teacherdataController();
            // data type 
            teachers selectedteacher = controller.Findteacher(id);

            return View(selectedteacher);
        }

        // post: /teachers/Update/{id}
        /// <summary>
        /// this will actually update the teacher details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult Update(int id, string teachersFName, string teachersLName, string salary, string hireDate)
        {
           // Debug.writeline()

            teachers TeacherInfo = new teachers();
            TeacherInfo.teacherfname = teachersFName;
            TeacherInfo.teacherlname = teachersLName;
            TeacherInfo.salary = salary;
            TeacherInfo.hiredate = hireDate;

            //Updating the teacher info
            teacherdataController Controller = new teacherdataController();
            Controller.UpdateTeacher(id, TeacherInfo);

            // it will return to the edited teacher
            return RedirectToAction("/show/" + id);  
        }
    }
}