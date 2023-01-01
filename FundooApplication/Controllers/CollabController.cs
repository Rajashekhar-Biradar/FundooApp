using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Context;

namespace FundooApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        
        ICollabBusiness collabBusiness;
        private readonly ILogger<CollabController> logger;
        public CollabController(ICollabBusiness collabBusiness, ILogger<CollabController> logger)
        {
           
            this.collabBusiness = collabBusiness;
            this.logger = logger;
        }

        [HttpPost("AddCollab")]
        
        public IActionResult AddCollab(long noteId,string email)
        {
            try
            {
                logger.LogInformation("Initialised the creating new collaborators");
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

                var result = collabBusiness.CreateCollab(noteId, email,userid);
                if (result != null)
                {
                    logger.LogInformation("Collaborator successfully added");
                    return this.Ok(new {Success = true, message = "Collaboration", data = result });
                }
                else
                {
                    logger.LogError("Getting error while adding new collaborator");
                    return this.BadRequest(new { Sucess = false, message = "Failed" });
                }

            }catch(Exception ex)
            {
                throw ex;
            }
           
        }

        [HttpDelete("RemoveCollab")]

        public IActionResult RemoveCollab(long collabId)
        {
            try
            {
                logger.LogInformation("started to remove collaborators");
                var result = collabBusiness.RemoveCollab(collabId);
                if (result == true)
                {
                    logger.LogInformation($"Remove collab {collabId}");
                    return this.Ok(new { Success = true, message = "Collaberator successfully removed" });
                }else if (result == false)
                {
                    logger.LogError("CollabID doesn't exist");
                    return this.Ok(new { Success = true, message = "Collab ID doesn't exist" });
                }
                else
                {
                    logger.LogError($"failed to remove {collabId}");
                    return this.BadRequest(new { Success = false, message = "Failed" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
                     
        }

        [HttpGet("GetAllCollab")]

        public IActionResult GetAllCollab(long noteId)
        {
            try
            {
                logger.LogInformation($" getting all noteID {noteId}");
                var result = collabBusiness.GetAllCollab(noteId);
                if (result != null)
                {
                    logger.LogInformation($"get all collabarators ");
                    return this.Ok(new { Success = true, message = "Get all Collabrators",data=result });
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
    }
}
