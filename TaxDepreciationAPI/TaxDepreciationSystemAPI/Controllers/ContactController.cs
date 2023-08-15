using BMTTaxDepreciation.Backend.Api.Authentication;
using BMTTaxDepreciation.Backend.Api.Models;
using TaxDepreciationSystem.Backend.Repository.Interfaces;
using TaxDepreciationSystem.Backend.Repository.Models;
using BMTTaxDepreciationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BMTTaxDepreciation.Backend.Api.Common.Enums;

namespace BMTTaxDepreciationAPI.Controllers {
    [Route("api/v1/contact")]
    [ApiController]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public class ContactController : Controller
    {
        private readonly IContactRepository dbRepository = null;
        public ContactController(IContactRepository contactRepository) : base()
        {
            this.dbRepository = contactRepository;
        }

        [HttpGet("{searchTerm}")]
        [HttpGet("")]
        public async Task<ApiResponse> Gets(string searchTerm ="")
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.Status = ApiResponseStatus.Success.ToString();

            try
            {
                var contacts = await this.dbRepository.GetContacts();

                apiResponse.Data = contacts.Where(x => x.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                                                       x.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                                                       x.CompanyName.ToLower().Contains(searchTerm.ToLower()) ||
                                                       x.Mobile.ToLower().Contains(searchTerm.ToLower()) ||
                                                       x.Email.ToLower().Contains(searchTerm.ToLower())
                                            ).Select(contact => new
                {
                    Id = contact.Id,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    CompanyName = contact.CompanyName,
                    Mobile = contact.Mobile,
                    Email = contact.Email
                }).ToList();
            }
            catch (Exception ex)
            {
                apiResponse.Status = ApiResponseStatus.Fail.ToString();
                apiResponse.Message = ex.Message;
            }

            return apiResponse;
        }


        

        [HttpPost("")]
        public async Task<ApiResponse> Save(Contact contactSubmitted)
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.Status = ApiResponseStatus.Success.ToString();
            try
            {
                Contact contact = new Contact();
                /* Insert */
                if (contactSubmitted?.Id == 0)
                {
                    contact.CreatedBy = 1; /* Should be the current user logged-in*/
                    contact.FirstName = contactSubmitted.FirstName;
                    contact.LastName = contactSubmitted.LastName;
                    contact.CompanyName = contactSubmitted.CompanyName;
                    contact.Mobile = contactSubmitted.Mobile;
                    contact.Email = contactSubmitted.Email;
                    this.dbRepository.Create(contact);
                }
                else
                {
                    contact = await this.dbRepository.GetAsync(Convert.ToInt32(contactSubmitted.Id));
                    contact.UpdatedBy = 1; /* Should be the current user logged-in*/
                    contact.UpdatedDate = DateTime.Now;
                    contact.FirstName = contactSubmitted.FirstName;
                    contact.LastName = contactSubmitted.LastName;
                    contact.CompanyName = contactSubmitted.CompanyName;
                    contact.Mobile = contactSubmitted.Mobile;
                    contact.Email = contactSubmitted.Email;
                    this.dbRepository.Update(contact);
                }

                await this.dbRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                apiResponse.Status = ApiResponseStatus.Fail.ToString();
                apiResponse.Message = ex.Message;
            }

            return apiResponse;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.Status = ApiResponseStatus.Success.ToString();
            try
            {
                Contact contact = await this.dbRepository.GetAsync(id);

               
                contact.IsDeleted = true;
                contact.UpdatedBy = 1; /* Should be the current user logged-in*/
                contact.UpdatedDate = DateTime.Now;
                this.dbRepository.Update(contact);

                await this.dbRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                apiResponse.Status = ApiResponseStatus.Fail.ToString();
                apiResponse.Message = ex.Message;
            }

            return apiResponse;
        }
    }
}
