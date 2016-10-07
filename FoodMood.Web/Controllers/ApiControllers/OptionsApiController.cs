using FoodMood.Web.Models.Domains;
using FoodMood.Web.Models.Requests;
using FoodMood.Web.Models.Responses;
using FoodMood.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodMood.Web.Controllers.ApiControllers
{
    [RoutePrefix("api/options")]
    public class OptionsApiController : ApiController
    {
        [Route()][HttpGet]
        public HttpResponseMessage GetAll()
        {
            try
            {
                ItemsResponse<Option> response = new ItemsResponse<Option>();
                response.Items = OptionsService.SelectAll();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                ErrorResponse response = new ErrorResponse(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
            
        }
        [Route("{id:int}")][HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                ItemResponse<Option> response = new ItemResponse<Option>();
                response.Item = OptionsService.SelectById(id);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                ErrorResponse response = new ErrorResponse(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }
        [Route, HttpPost]
        public HttpResponseMessage Post(OptionAddRequest model)
        {
            if (!ModelState.IsValid && model != null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                int id = OptionsService.Insert(model);
                ItemResponse<int> response = new ItemResponse<int>();
                response.Item = id;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                ErrorResponse response = new ErrorResponse(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Put(OptionUpdateRequest model)
        {
            if (!ModelState.IsValid && model != null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                OptionsService.Update(model);
                SuccessResponse sr = new SuccessResponse();
                return Request.CreateResponse(HttpStatusCode.OK, sr);
            }
            catch (Exception ex)
            {
                ErrorResponse response = new ErrorResponse(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }


        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                OptionsService.Delete(id);
                SuccessResponse sr = new SuccessResponse();
                return Request.CreateResponse(HttpStatusCode.OK, sr);
            }
            catch (Exception ex)
            {
                ErrorResponse response = new ErrorResponse(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }
    }
}
