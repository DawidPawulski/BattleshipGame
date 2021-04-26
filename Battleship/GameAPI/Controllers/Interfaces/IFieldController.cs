using System.Collections.Generic;
using System.Threading.Tasks;
using GameAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers.Interfaces
{
    public interface IFieldController
    {
        Task<ActionResult<IEnumerable<Field>>> GetFields();
        Task<ActionResult<Field>> GetField(int id);
        Task<ActionResult> PutField(int id, Field field);
        Task<ActionResult<Field>> PostField(Field field);
        Task<ActionResult<Field>> DeleteField(int id);
    }
}