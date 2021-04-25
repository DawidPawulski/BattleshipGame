using System.Collections.Generic;
using System.Threading.Tasks;
using GameAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers.Interfaces
{
    public interface IFieldController
    {
        public Task<ActionResult<IEnumerable<Field>>> GetFields();
        public Task<ActionResult<Field>> GetField(int id);
        public Task<ActionResult> PutField(int id, Field field);
        public Task<ActionResult<Field>> PostField(Field field);
        public Task<ActionResult<Field>> DeleteField(int id);
    }
}