using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepoLayer.Context;
using RepoLayer.Entity;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace FundooApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        INoteBusiness noteBusiness;
        private readonly ILogger<NoteController> logger;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private string KEY = "Shekhar";
        public NoteController(INoteBusiness node, ILogger<NoteController> logger,IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.noteBusiness = node;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.logger = logger;
            logger.LogDebug(1, "NLog injected into HomeController");
        }


        [HttpPost("CreateNote")]
        public IActionResult CreateNote(NoteModel UserData)
        {
            logger.LogInformation("initialize the Creating a new notes");
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

                var result = noteBusiness.CreateNote(UserData, userid);
                if (result != null)
                {
                    logger.LogInformation("Note added sucessfully");
                    return this.Ok(new { Success = true, message = "Note added successfully", data = result });
                }
                else
                {
                    logger.LogInformation("Faild to create note");
                    return this.BadRequest(new { Success = false, message = "failed" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetAllNote")]
       public IActionResult GetAllNotes()
        {
            this.logger.LogInformation("Getting all notes");
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = noteBusiness.GetAllNotes(userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Get all note successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "failed" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("UpdateNote")]
        public IActionResult UpdateNotes(long noteId, NoteModel note) 
        {
            this.logger.LogInformation("Update the old notes");
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = noteBusiness.UpdateNotes(userId, noteId, note);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Note updated Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Failed" });
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("RemoveNote")]

        public IActionResult RemoveNotes(long userId,long noteId)
        {
            logger.LogInformation("Removing the existing notes");
            try
            {
                var result = noteBusiness.RemoveNotes(userId, noteId);
                if (result == true)
                {
                    return this.Ok(new { Success = true, message = "Note removed successfully" });
                }
                else if(result == false)
                {
                    return this.Ok(new { Success = true, message = "NoteId doesn't exist" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Failed" });
                }
            }
            catch(Exception ex) 
            { 
                throw ex;
            }
        }

        [HttpPut("ChangeColor")]

        public IActionResult ChangeColor(long noteId,string color)
        {
            this.logger.LogInformation("Changing the Background color");
            try
            {
                var result = noteBusiness.ChangeColor(noteId, color);
                if(result != null)
                {
                    return this.Ok(new { Success = true, message = "Color changed Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "false" });
                }
            }
            catch(Exception ex)        
            { 
                throw ex;
            }

        }

        [HttpPut("IsPin")]

        public IActionResult IsPinOrNot(long noteId)
        {
            try
            {
                var result = noteBusiness.IsPinOrNot(noteId);
                if (result == true)
                {
                    return this.Ok(new { Success = true, message = "Pin change off to on " });
                }else if (result == false) 
                {
                    return this.Ok(new { Success = true, message = "pin change on to off" });
                }
                else
                {
                    return this.BadRequest(new {Success = false, message = "failed"});
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("IsTrash")]

        public IActionResult IsTrashOrNot(long noteId)
        {
            try
            {
                var result = noteBusiness.IsTrashOrNot(noteId);
                if (result == true)
                {
                    return this.Ok(new { Success = true, message = "Trash change off to on " });
                }
                else if (result == false)
                {
                    return this.Ok(new { Success = true, message = "Trash change on to off" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("IsArchive")]

        public IActionResult IsArchiveOrNot(long noteId)
        {
            try
            {
                var result = noteBusiness.IsArchiveOrNot(noteId);
                if (result == true)
                {
                    return this.Ok(new { Success = true, message = "Note archived " });
                }
                else if (result == false)
                {
                    return this.Ok(new { Success = true, message = "Action not done" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("UploadImage")]
        public IActionResult UploadImage(long NoteId, long userId, IFormFile img)
        {
            try
            {
                var result = noteBusiness.UploadImage(NoteId, userId, img);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Image uploaded successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Failed" });
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("RemoveTrashForever")]

        public IActionResult RemoveTrashForever(long NoteId)
        {
            try
            {
                var result = noteBusiness.RemoveTrashForever(NoteId);
                if (result == false)
                {
                    return this.Ok(new { Success = true, message = "Note Successfully removed from Trash" });
                }
                else if (result == true)
                {
                    return this.Ok(new { Success = true, message = "Note successfully added to Trash" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "invaid Note Id" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetAllNoteByRedisCache")]

        public async Task<IActionResult> GetAllNoteByRediCache()
        {
            try
            {
                string serializeNote;
                var NoteList = new List<NoteEntity>();
                var RedisNoteList = await distributedCache.GetAsync(KEY);
                if (RedisNoteList != null)
                {
                    serializeNote = Encoding.UTF8.GetString(RedisNoteList);
                    NoteList = JsonConvert.DeserializeObject<List<NoteEntity>>(serializeNote);
                }
                else
                {
                    NoteList = await this.noteBusiness.GetAllNoteByRedisCache();
                    serializeNote = JsonConvert.SerializeObject(NoteList);
                    RedisNoteList = Encoding.UTF8.GetBytes(serializeNote);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(100))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                    await distributedCache.SetAsync(KEY, RedisNoteList, options);
                }
                return this.Ok(new { Success = true, message = "Get all notes successfully", data = NoteList });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
