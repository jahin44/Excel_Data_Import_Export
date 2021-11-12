using Autofac;
using DataImporter.Common.Utilities;
using DataImporter.Web.Models.GroupModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using OfficeOpenXml;
using System.Data;

namespace DataImporter.Web.Controllers
{
    [Authorize(Policy = "ViewPermission")]
    public class GroupController : Controller
    {
        private readonly ILogger<GroupController> _logger;
        private IWebHostEnvironment _hostingEnvironment;
        public GroupController(ILogger<GroupController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var model = new GroupListModel();
            return View(model);
        }

        public JsonResult GetGroupData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new GroupListModel();
            var data = model.GetGroups(dataTablesModel);
            return Json(data);
        }
        public IActionResult Contacts()
        {
            var model = new ContactsListModel();
            return View(model);
        }

        public JsonResult GetFileStatusData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new ContactsListModel();
            var data = model.GetFileStatuss(dataTablesModel);
            return Json(data);
        }
        public IActionResult Exports()
        {
            var model = new ExportHistoryListModel();
            return View(model);
        }

        public JsonResult GetExportHistoryData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new ExportHistoryListModel();
            var data = model.GetExportHistorys(dataTablesModel);
            return Json(data);
        }

        public IActionResult Create()
        {
            var model = new CreateGroupModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateGroupModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateGroup();
                    ModelState.AddModelError("", "successfully Group Created");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create group");
                    _logger.LogError(ex, "Create Group Failed");
                }
            }
            return View(model);
        }        

        
        [HttpGet]
        public IActionResult Preview(int id)
        {
            var model = new ImportGroupModel();
           
            model.LoadModelData(id);

            return View(model);
        }

        [HttpPost]
        public IActionResult Preview(ImportGroupModel model)
        {
            IFormFile file = Request.Form.Files[0];
            model.FileUrl = Path.Combine(_hostingEnvironment.WebRootPath, "UploadExcel");
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateFileStatus();
                    ModelState.AddModelError("", "File Upload successful");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to Upload File");
                    _logger.LogError(ex, "Same File, Failed");
                }

                return View(model);
            }
 
            
            var Tabledata = model.TableHtmlData(file, model.FileUrl);
            return this.Content(Tabledata);

        }

        public IActionResult ExportGroup(int id)
        {
            var model = new ExportGroupModel();
            model.LoadModelData(id);
            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult ExportGroup(ExportGroupModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable = new DataTable();
                    var datatable =model.ExportAndEmail(model.Id,dataTable);
                    
                    byte[] bytes = null;
                    var stream = new MemoryStream();
                    
                    using (ExcelPackage pck = new ExcelPackage(stream))
                    {
                        var ws = pck.Workbook.Worksheets.Add("Accounts");
                        ws.Cells["A1"].LoadFromDataTable(dataTable, true);                        
                        pck.Save();
                    }

                    stream.Position = 0;
                    var FileName = model.GroupName+ DateTime.Now.ToString("yyyyMMddHHmmssfff")+".xlsx";                    
                    bytes = stream.ToArray();
                    
                    model.FileEmail(bytes, FileName,model.Email);
                   
                    ModelState.AddModelError("", "successfully Group Created");
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create group");
                    _logger.LogError(ex, "Create Group Failed");
                }
            }
            return View(model);
        }


        public IActionResult ShowGroupTable(int id)
        {
            var model = new ShowGroupTableModel();

            model.LoadModelData(id);
            
            return View(model);
        }


        //[HttpPost]
        //public IActionResult ShowGroupTable(ShowGroupTableModel model)
        //{
        //    string HtmlTableData = "<table class='table table-bordered'><tr><th>Address</th><th>Phone</th><th>FirstName</th>" +
        //        "<th>LastName</th></tr><tr>\r\n<td>Dhaka</td><td>17258963</td><td>Jahin</td><td>Hasan</td></tr>\r\n<td>Mymenshingh</td><td>17596566</td><td>Jahid </td><td>Moon</td></tr>\r\n<td>Nandiel</td><td>1789654</td><td>Noor</td><td>Chowdhury</td></tr>\r\n<td>Jamalpur</td><td>1789654</td><td>Israt</td><td>Supty</td></tr>\r\n</table>";
        //    return this.Content( HtmlTableData);

        //}


        public IActionResult Edit(int id)
        {
            var model = new EditGroupModel();
            model.LoadModelData(id);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditGroupModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Update();
                    ModelState.AddModelError("", "successfully Group Update");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to Update group");
                    _logger.LogError(ex, "Create Group Failed");
                }
            }
            return View(model);
            
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var model = new GroupListModel();

            model.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
