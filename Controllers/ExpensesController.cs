using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinances.Data;
using PersonalFinances.Data.Services;
using PersonalFinances.Models;

namespace PersonalFinances.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ExpensesService expensesService;

        public ExpensesController(ExpensesService expensesService)
        {
            this.expensesService = expensesService;
        }
        public async Task<IActionResult> Index()
        {
            // Get all expenses nga databaza
            var expenses = await expensesService.GetAllExpensesAsync();

            return View(expenses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Expense expense)
        {
            if (ModelState.IsValid)
            {
                await expensesService.Add(expense);

                return RedirectToAction("Index");
            }


            return View(expense);
        }

        public IActionResult GetChart()
        {
            var data = expensesService.GetChartData();
            
            return Json(data);
            
        }

        public async Task<IActionResult> Delete(int id)
        {
            var expense = await expensesService.GetExpenseByIdAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense); // show confirmation page
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await expensesService.DeleteExpenseAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
