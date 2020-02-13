using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using PennyTracker.Shared.Models;
using PennyTracker.BlazorServer.Services;
using PennyTracker.BlazorServer.ViewModels;

namespace PennyTracker.BlazorServer.Tests
{
    [TestClass]
    public class CreateExpenseViewModelTests
    {
        private Expense expense;

        [TestInitialize]
        public void Initialize()
        {
            this.expense = new Expense
            {
                Id = 1,
                Amount = 100,
                Category = Category.Auto,
                CreationDate = DateTime.UtcNow,
                SpentDate = DateTime.UtcNow,
                Description = "Petrol"
            };
        }

        [TestMethod]
        public void OnInitialzed_WhenCreateNew_ShouldModelBeInstatiated()
        {
            //arrange
            var expenseServiceMock = new Mock<IExpenseService>();
            var dialogServiceMock = new Mock<IDialogService>();

            var vm = new CreateExpenseViewModel(dialogServiceMock.Object, expenseServiceMock.Object);
            vm.OnInitializeAsync().GetAwaiter().GetResult();

            //assert
            Assert.IsNotNull(vm.Model); 
            Assert.IsNotNull(vm.Model.SpentDate); 
        }

        [TestMethod]
        public void OnInitialzed_WhenEdit_ShouldModelBeInstatiated()
        {
            //arrange
            var expenseServiceMock = new Mock<IExpenseService>();
            expenseServiceMock.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(this.expense);

            var dialogServiceMock = new Mock<IDialogService>();

            var vm = new CreateExpenseViewModel(dialogServiceMock.Object, expenseServiceMock.Object);
            vm.OnInitializeAsync(this.expense.Id).GetAwaiter().GetResult();

            //assert
            Assert.IsNotNull(vm.Model);
            Assert.AreEqual(this.expense.Id, vm.Model.Id);
            Assert.AreEqual(this.expense.Category, vm.Model.Category);
            Assert.AreEqual(this.expense.CreationDate, vm.Model.CreationDate);
            Assert.AreEqual(this.expense.SpentDate, vm.Model.SpentDate);
            Assert.AreEqual(this.expense.Description, vm.Model.Description);
        }

        [TestMethod]
        public void OnButtonSave_WhenCreateNew_ShouldCallAddExpense()
        {
            //arrange
            var expenseServiceMock = new Mock<IExpenseService>();
            expenseServiceMock.Setup(x => x.AddAsync(It.IsAny<Expense>()));

            var dialogServiceMock = new Mock<IDialogService>();
            dialogServiceMock.Setup(x => x.Close(It.IsAny<bool>()));

            var vm = new CreateExpenseViewModel(dialogServiceMock.Object, expenseServiceMock.Object);
            vm.OnInitializeAsync(this.expense.Id).GetAwaiter().GetResult();

            //act
            vm.OnButtonSaveClickAsync().GetAwaiter().GetResult();

            //assert
            expenseServiceMock.Verify(x => x.AddAsync(vm.Model));
            dialogServiceMock.Verify(x => x.Close(true));
        }

        [TestMethod]
        public void OnButtonSave_WhenEdit_ShouldCallUpdateExpense()
        {
            //arrange
            var expenseServiceMock = new Mock<IExpenseService>();
            expenseServiceMock.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(this.expense);
            expenseServiceMock.Setup(x => x.AddAsync(It.IsAny<Expense>()));

            var dialogServiceMock = new Mock<IDialogService>();
            dialogServiceMock.Setup(x => x.Close(It.IsAny<bool>()));

            var vm = new CreateExpenseViewModel(dialogServiceMock.Object, expenseServiceMock.Object);
            vm.OnInitializeAsync(this.expense.Id).GetAwaiter().GetResult();

            //act
            vm.OnButtonSaveClickAsync().GetAwaiter().GetResult();

            //assert
            expenseServiceMock.Verify(x => x.UpdateAsync(vm.Model.Id, vm.Model));
            dialogServiceMock.Verify(x => x.Close(true));
        }

        [TestMethod]
        public void OnButtonCancel_ShouldCallDialogServiceClose()
        {
            //arrange
            var expenseServiceMock = new Mock<IExpenseService>();

            var dialogServiceMock = new Mock<IDialogService>();
            dialogServiceMock.Setup(x => x.Close(It.IsAny<bool>()));

            var vm = new CreateExpenseViewModel(dialogServiceMock.Object, expenseServiceMock.Object);
            vm.OnInitializeAsync().GetAwaiter().GetResult();

            //act
            vm.OnButtonCancelClicked();

            //assert
            dialogServiceMock.Verify(x => x.Close(false));
        }
    }
}
