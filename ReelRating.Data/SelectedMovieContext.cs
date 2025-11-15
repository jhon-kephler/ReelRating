using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelectedMovie.Data
{
    public class SelectedMovieContext : DbContext
    {
        public SelectedMovieContext(DbContextOptions<SelectedMovieContext> options) : base(options) { }
    }
}
