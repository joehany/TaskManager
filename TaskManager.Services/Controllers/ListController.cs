using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManager.Infrastructure.Data.Entities;
using TaskManager.Infrastructure.Interfaces.Managers;

namespace TaskManager.Services.Controllers
{
    public class ListController : ApiController
    {
        private readonly IListManager _listManager;

        public ListController(IListManager listManager)
        {
            _listManager = listManager;
        }
        
        public HttpResponseMessage Get(Guid id)
        {
            try
            {
                var list = _listManager.GetListById(id);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Post()
        {
            try
            {
                var list = _listManager.CreateList();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}