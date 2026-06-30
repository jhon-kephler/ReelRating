using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Command.ReviewCommand
{
    public interface IUpdateReviewCommand { Task UpdateReview(int id, Review review); }

    public class UpdateReviewCommand : IUpdateReviewCommand
    {
        private readonly IRepository<Review> _repository;

        public UpdateReviewCommand(IRepository<Review> repository)
        {
            _repository = repository;
        }

        public Task UpdateReview(int id, Review review)
        {
            _repository.Update(id, review);
            return Task.CompletedTask;
        }
    }
}
