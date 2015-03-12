using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TaskManager.Infrastructure.Data.Entities;
using TaskManager.Infrastructure.Interfaces.Managers;

namespace TaskManager.Services.Controllers
{
    public class TaskController : ApiController
    {
        private readonly IListManager _listManager;

        public TaskController(IListManager listManager)
        {
            _listManager = listManager;
        }

        public HttpResponseMessage Post([FromBody]TaskItem newtTask)
        {
            try
            {
                var task = _listManager.CreateTask(newtTask);
                return Request.CreateResponse(HttpStatusCode.OK, task);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Put(Guid id, [FromBody]TaskItem updatedTask)
        {
            try
            {
                _listManager.UpdateTask(id, updatedTask);
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                _listManager.DeleteTask(id);
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}