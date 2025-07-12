using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinances.Models;

namespace PersonalFinances.Data.Services
{
    public class ExpensesService
    {
        private readonly AppDbContext _context;

        public ExpensesService(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(Expense expense)
        {
            if (expense == null)
            {
                throw new ArgumentNullException(nameof(expense), "Expense cannot be null");
            }
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            var expenses = await _context.Expenses.ToListAsync();
            return expenses;
        }

        public List<object> GetChartData()
        {
            var data = _context.Expenses
                .GroupBy(e => e.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    TotalAmount = g.Sum(e => e.Amount)
                }).ToList<object>();

            return data;
        }

        public async Task<Expense> GetExpenseByIdAsync(int id)
        {
            return await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task DeleteExpenseAsync(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
        }

    }
}
