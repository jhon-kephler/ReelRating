using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Command.ReviewCommand
{
    public interface ICreateReviewCommand { Task AddReview(Review request); }

    public class CreateReviewCommand : ICreateReviewCommand
    {
        private readonly IRepository<Review> _repository;

        public CreateReviewCommand(IRepository<Review> repository)
        {
            _repository = repository;
        }

        public async Task AddReview(Review request)
        {
            _repository.Add(request);
        }
    }
}
