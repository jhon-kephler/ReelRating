using AutoMapper;
using ReelRating.Application.Services.ReviewServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.ReviewSchema.Request;
using ReelRating.Data.Command.ReviewCommand;
using ReelRating.Data.Query.CineQuery;
using ReelRating.Data.Query.CustomerQuery;
using ReelRating.Data.Query.ReviewQuery;
using ReelRating.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Services.ReviewServices
{
    public class ManageReviewService : IManageReviewService
    {
        private readonly IMapper _mapper;
        private readonly ICreateReviewCommand _createReviewCommand;
        private readonly IGetCineByIdQuery _getCineByIdQuery;
        private readonly IGetCustomerByIdQuery _getCustomerByIdQuery;
        private readonly IGetReviewByIdQuery _getReviewByIdQuery;
        private readonly IUpdateReviewCommand _updateReviewCommand;

        public ManageReviewService(IMapper mapper, ICreateReviewCommand createReviewCommand, IGetCineByIdQuery getCineByIdQuery, IGetCustomerByIdQuery getCustomerByIdQuery, IGetReviewByIdQuery getReviewByIdQuery, IUpdateReviewCommand updateReviewCommand)
        {
            _mapper = mapper;
            _createReviewCommand = createReviewCommand;
            _getCineByIdQuery = getCineByIdQuery;
            _getCustomerByIdQuery = getCustomerByIdQuery;
            _getReviewByIdQuery = getReviewByIdQuery;
            _updateReviewCommand = updateReviewCommand;
        }

        public async Task<Result> CreateReview(CreateReviewRequest request)
        {
            var result = new Result();

            try
            {
                if (await ValidateRequest(request))
                    await _createReviewCommand.AddReview(_mapper.Map<Review>(request));
                else
                {
                    result.ValidateResult("Invalid request");
                    return result;
                }

                result.SetSuccess(true);
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred during the search: {ex.Message}");
            }

            return result;
        }

        public async Task<Result> UpdateReview(UpdateReviewRequest request)
        {
            var result = new Result();

            try
            {
                var review = await _getReviewByIdQuery.GetReviewById(request.Id);
                if (review == null)
                {
                    result.ValidateResult($"Review with id '{request.Id}' not found.", 404);
                    return result;
                }

                review.REVIEW = request.REVIEW;
                review.Note = request.Note;

                await _updateReviewCommand.UpdateReview(request.Id, review);

                result.SetSuccess(true);
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred during the search: {ex.Message}");
            }
            return result;
        }

        public async Task<Result> DeleteReview(DeleteReviewRequest request)
        {
            var result = new Result();

            try
            {
                var review = await _getReviewByIdQuery.GetReviewById(request.Id);
                if (review == null)
                {
                    result.ValidateResult($"Review with id '{request.Id}' not found.", 404);
                    return result;
                }

                review.Deleted = true;

                await _updateReviewCommand.UpdateReview(request.Id, review);

                result.SetSuccess(true);
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred during the search: {ex.Message}");
            }
            return result;
        }

        private async Task<bool> ValidateRequest(CreateReviewRequest request)
        {
            var result = new bool();
            try
            {
                result = true;

                var cine = await _getCineByIdQuery.GetCineByIdAsync(request.CineId);
                if (cine == null)
                    result = false;

                var customer = await _getCustomerByIdQuery.GetCustomerById(request.CustomerId);
                if (customer == null)
                    result = false;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during the validation: {ex.Message}");
            }
            return result;
        }
    }
}