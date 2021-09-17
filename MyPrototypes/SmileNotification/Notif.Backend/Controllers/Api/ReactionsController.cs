using Microsoft.AspNetCore.Mvc;
using Notif.Backend.Data.Repositories.Contracts;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Notif.Backend.Helpers;
using Notif.Backend.Models; 
//using Notif.Transversal.Models;

namespace Notif.Backend.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReactionsController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IReaction _reactionRepository;

        public ReactionsController(IReaction reactionRepository,IUserHelper userHelper)
        {
            _userHelper = userHelper;
            _reactionRepository = reactionRepository;
        }

    
        [HttpGet]
        public IActionResult GetReactions()
        {
            return Ok(_reactionRepository.GetAllWithUsers());
        }


       [HttpPost]
        public async Task<IActionResult> PostReaction([FromBody] Notif.Transversal.Models.ReactionResponse reaction)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }
            ApplicationUser user;
            if (reaction.User != null)
            {
                user= await _userHelper.GetUserByEmailAsync(reaction.User.Email);
                if (user == null)
                {
                    return this.BadRequest("Invalid user");
                }
            }
            else
            {
                return this.BadRequest("user data is empty.");
            }
            //TODO: Upload images
            var entityReaction = new Reaction
            {
                Punctuation = reaction.Punctuation,
                Name = reaction.Name,
                Email = reaction.Email,
                Observation = reaction.Observation,
                ApplicationUser = user
            };

            var newProduct = await _reactionRepository.CreateAsync(entityReaction);
            return Ok(newProduct);
        }

        //public async Task<Response> PutAsync<T>(
        //    string urlBase,
        //    string servicePrefix,
        //    string controller,
        //    int id,
        //    T model,
        //    string tokenType,
        //    string accessToken)
        //{
        //    try
        //    {
        //        var request = JsonConvert.SerializeObject(model);
        //        var content = new StringContent(request, Encoding.UTF8, "application/json");
        //        var client = new HttpClient
        //        {
        //            BaseAddress = new Uri(urlBase)
        //        };

        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
        //        var url = $"{servicePrefix}{controller}/{id}";
        //        var response = await client.PutAsync(url, content);
        //        var answer = await response.Content.ReadAsStringAsync();
        //        if (!response.IsSuccessStatusCode)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = answer,
        //            };
        //        }

        //        var obj = JsonConvert.DeserializeObject<T>(answer);
        //        return new Response
        //        {
        //            IsSuccess = true,
        //            Result = obj,
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response
        //        {
        //            IsSuccess = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        //public async Task<Response> DeleteAsync(
        //    string urlBase,
        //    string servicePrefix,
        //    string controller,
        //    int id,
        //    string tokenType,
        //    string accessToken)
        //{
        //    try
        //    {
        //        var client = new HttpClient
        //        {
        //            BaseAddress = new Uri(urlBase)
        //        };

        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
        //        var url = $"{servicePrefix}{controller}/{id}";
        //        var response = await client.DeleteAsync(url);
        //        var answer = await response.Content.ReadAsStringAsync();
        //        if (!response.IsSuccessStatusCode)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = answer,
        //            };
        //        }

        //        return new Response
        //        {
        //            IsSuccess = true
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response
        //        {
        //            IsSuccess = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutReaction([FromRoute] int id, [FromBody] TagHelperMetadata.Common.Models.reaction reaction)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return this.BadRequest(ModelState);
        //    }

        //    if (id != reaction.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var oldReaction = await this.reactionRepository.GetByIdAsync(id);
        //    if (oldReaction == null)
        //    {
        //        return this.BadRequest("reaction Id don't exists.");
        //    }

        //    //TODO: Upload images
        //    oldReaction.IsAvailabe = reaction.IsAvailabe;
        //    oldReaction.LastPurchase = reaction.LastPurchase;
        //    oldReaction.LastSale = reaction.LastSale;
        //    oldReaction.Name = reaction.Name;
        //    oldReaction.Price = reaction.Price;
        //    oldReaction.Stock = reaction.Stock;

        //    var updatedReaction = await this.reactionRepository.UpdateAsync(oldReaction);
        //    return Ok(updatedReaction);
        //}


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteReaction([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return this.BadRequest(ModelState);
        //    }

        //    var reaction = await this.reactionRepository.GetByIdAsync(id);
        //    if (reaction == null)
        //    {
        //        return this.NotFound();
        //    }

        //    await this.reactionRepository.DeleteAsync(reaction);
        //    return Ok(reaction);
        //}

    }
}