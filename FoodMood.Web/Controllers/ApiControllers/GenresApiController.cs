﻿using FoodMood.Web.ApiControllers.Controllers;
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
    [RoutePrefix("api/genres")]
    public class GenresApiController : BaseApiController
    {
        [Route, HttpGet]
        public HttpResponseMessage GetAll()
        {
            try
            {
                ItemsResponse<Genre> response = new ItemsResponse<Genre>();
                response.Items = GenresService.SelectAll();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                ItemResponse<Genre> response = new ItemResponse<Genre>();
                response.Item = GenresService.SelectById(id);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route, HttpPost]
        public HttpResponseMessage Post(GenreAddRequest model)
        {
            if(!ModelState.IsValid && model != null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                ItemResponse<int> response = new ItemResponse<int>();
                response.Item = GenresService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                ErrorResponse response = new ErrorResponse(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }
        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Put(GenreUpdateRequest model)
        {
            if (!ModelState.IsValid && model != null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                GenresService.Update(model);
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
                GenresService.Delete(id);
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
