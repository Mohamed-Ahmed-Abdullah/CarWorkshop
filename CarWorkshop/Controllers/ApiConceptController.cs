using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;
using CarWorkshop.DataAccess;
using System.Web.Hosting;
using System.Web.Mvc;
using CarWorkshop.DataAccess.Entities;
using Newtonsoft.Json.Linq;

namespace CarWorkshop.Controllers
{
    public class ApiConceptController : ApiController
    {
        public IEnumerable<Concept> Get()
        {
            var list = new DatabaseContext().Concepts.ToList();
            return list;
        }

        public Concept Get(int id)
        {
            return new DatabaseContext().Concepts.Single(w => w.Id == id);
        }


        #region  Upload Image 

        //<div class="row">
        //                   <input type = "file" id="file1" multiple="multiple" ng-model="concept.Image" />
        //               </div>

        //        var files = $("#file1").get(0).files;
        //                var data = new FormData();

        //                if (files.length > 0)
        //                {
        //                    for (i = 0; i<files.length; i++) {
        //                        data.append("file" + i, files[i]);                        
        //                    }
        //}

        //                $.ajax({
        //    url: "http:////" + window.location.host + "/Api/ApiConcept/Post",
        //                    type: 'POST',
        //                    contentType: false,
        //                    processData: false,
        //                    dataType: 'json',
        //                    data: data,
        //                    success: function(data) {
        //        conceptModal.modal('hide');
        //                        $scope.getList();
        //    },
        //                    error: function(x, y, z) {
        //        alert("ERROR:" + x + '\n' + y + '\n' + z);
        //    }
        //});

        //public async Task Post()
        //{
        //    byte[] image = null;
        //    if (Request.Content.IsMimeMultipartContent())
        //    {
        //        var fullPath = HttpContext.Current.Server.MapPath("~/App_Data");
        //        var streamProvider = new MultipartFormDataStreamProvider(fullPath);

        //        await Request.Content.ReadAsMultipartAsync(streamProvider);

        //        var fileData = streamProvider.FileData.FirstOrDefault();

        //        var info = new FileInfo(fileData.LocalFileName);
        //        image = File.ReadAllBytes(info.FullName);
        //    }
        //    else
        //    {
        //        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "Invalid Request!"));
        //    }
        //}
        #endregion

        public void Post([FromBody] Concept concept)
        {
            if (concept == null)
                throw new Exception("Concept Should not be null");

            var db = new DatabaseContext();

            //Save or Update?
            if (concept.Id == 0)
            {
                db.Concepts.Add(concept);
            }
            else
            {
                var conceptToUpdate = db.Concepts.FirstOrDefault(s => s.Id == concept.Id);
                if (conceptToUpdate == null)
                    throw new Exception("Concept you want to update is not exists");

                conceptToUpdate.Name = concept.Name;
                conceptToUpdate.Value = concept.Value;
            }

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var db = new DatabaseContext();
            var concept = db.Concepts.FirstOrDefault(f => f.Id == id);

            if (concept == null)
                throw new Exception("the concept that you want to delete is not exists in the db in the first place.");

            db.Concepts.Remove(concept);
            db.SaveChanges();
        }
    }
}
