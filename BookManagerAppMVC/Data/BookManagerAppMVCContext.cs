using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookManagerAppMVC.Models;

namespace BookManagerAppMVC.Data
{
    public class BookManagerAppMVCContext : DbContext
    {
        public BookManagerAppMVCContext (DbContextOptions<BookManagerAppMVCContext> options)
            : base(options)
        {
        }

        public DbSet<BookManagerAppMVC.Models.Book> Book { get; set; } = default!;
    }
}
