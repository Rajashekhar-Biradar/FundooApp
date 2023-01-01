using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Context;
using RepoLayer.Entity;

namespace FundooApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        
        ILabelBusiness labelBusiness;
        private readonly ILogger<LabelController> logger;
        public LabelController(ILabelBusiness labelBusiness,ILogger<LabelController> logger)
        {
           
            this.labelBusiness = labelBusiness;
            this.logger = logger;
        }

        [HttpPost("CreateLabel")]

        public IActionResult CreateLabel(LabelModel labelName, long noteId)
        {
            logger.LogInformation("Creating new labels");

            try 
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

                var result = labelBusiness.CreateLabel(labelName,noteId, userid);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "New label created" });
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
        
        [HttpPut("RenameLabel")]
        public IActionResult RenameLable(string OldLableName, string NewLableName, long userId)
        {
            try
            {
                var result = labelBusiness.RenameLable(OldLableName, NewLableName, userId);
                if (result == true)
                {
                    return this.Ok(new { Success = true, message = "Label successfully Renamed" });
                }else if (result == false)
                {
                    return this.Ok(new { Success = true, message = "LabelName or UserId doesn't exist"});
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

        [HttpDelete("RemoveLable")]

        public IActionResult RemoveLable(string labelName, long userId)
        {
            try
            {
                var result = labelBusiness.RemoveLable(labelName, userId);
                if (result == true)
                {
                    return this.Ok(new { Success = true, message = "Label successfully Removed" });
                }
                else if (result == false)
                {
                    return this.Ok(new { Success = true, message = "LabelName or UserId doesn't exist" });
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

        [HttpGet("GetAllLabels")]
        public IActionResult GetAllLabels(long userId)
        {
            try
            {
                var result = labelBusiness.GetAllLabels(userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Get all labels successfully" ,data =result });
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
