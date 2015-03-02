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

        public TaskList Get(Guid id)
        {
            return _listManager.GetListById(id);
        }

        public void Post([FromBody]string value)
        {
        }

        public void Put(int id, [FromBody]string value)
        {
        }

        public void Delete(int id)
        {
        }
    }
}