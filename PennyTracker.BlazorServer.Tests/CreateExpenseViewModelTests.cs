using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using PennyTracker.Shared.Models;
using PennyTracker.BlazorServer.Services;
using PennyTracker.BlazorServer.ViewModels;
using System.Threading.Tasks;

namespace PennyTracker.BlazorServer.Tests
{
    [TestClass]
    public class CreateExpenseViewModelTests
    {
        private Expense editExpense;
        private Expense newExpense;

        [TestInitialize]
        public void Initialize()
        {
            this.editExpense = new Expense
            {
                Id = 1,
                Amount = 100,
                Category = Category.Auto,
                CreationDate = DateTime.UtcNow,
                SpentDate = DateTime.UtcNow,
                Description = "Petrol"
            };

            this.newExpense = new Expense { SpentDate = DateTime.UtcNow };
        }

        [TestMethod]
        public void OnInitialzed_WhenCreateNew_ShouldModelBeInstatiated()
        {
            //arrange
            var expenseServiceMock = new Mock<IExpenseService>();
            var dialogServiceMock = new Mock<IDialogService>();

            var vm = new CreateExpenseViewModel(dialogServiceMock.Object, expenseServiceMock.Object);
            vm.Model = this.newExpense;

            //assert
            Assert.IsNotNull(vm.Model); 
            Assert.IsNotNull(vm.Model.SpentDate); 
        }

        [TestMethod]
        public void OnInitialzed_WhenEdit_ShouldModelBeInstatiated()
        {
            //arrange
            var expenseServiceMock = new Mock<IExpenseService>();
            expenseServiceMock.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(this.editExpense);

            var dialogServiceMock = new Mock<IDialogService>();

            var vm = new CreateExpenseViewModel(dialogServiceMock.Object, expenseServiceMock.Object);
            vm.Model = this.editExpense;

            //assert
            Assert.IsNotNull(vm.Model);
            Assert.AreEqual(this.editExpense.Id, vm.Model.Id);
            Assert.AreEqual(this.editExpense.Category, vm.Model.Category);
            Assert.AreEqual(this.editExpense.CreationDate, vm.Model.CreationDate);
            Assert.AreEqual(this.editExpense.SpentDate, vm.Model.SpentDate);
            Assert.AreEqual(this.editExpense.Description, vm.Model.Description);
        }

        [TestMethod]
        public async Task OnButtonSave_WhenCreateNew_ShouldCallAddExpense()
        {
            //arrange
            var expenseServiceMock = new Mock<IExpenseService>();
            expenseServiceMock.Setup(x => x.AddAsync(It.IsAny<Expense>()));

            var dialogServiceMock = new Mock<IDialogService>();
            dialogServiceMock.Setup(x => x.Close(It.IsAny<bool>()));

            var vm = new CreateExpenseViewModel(dialogServiceMock.Object, expenseServiceMock.Object);
            vm.Model = this.newExpense;

            //act
            await vm.OnButtonSaveClickAsync();

            //assert
            expenseServiceMock.Verify(x => x.AddAsync(vm.Model));
            dialogServiceMock.Verify(x => x.Close(true));
        }

        [TestMethod]
        public async Task OnButtonSave_WhenEdit_ShouldCallUpdateExpense()
        {
            //arrange
            var expenseServiceMock = new Mock<IExpenseService>();
            expenseServiceMock.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(this.editExpense);
            expenseServiceMock.Setup(x => x.AddAsync(It.IsAny<Expense>()));

            var dialogServiceMock = new Mock<IDialogService>();
            dialogServiceMock.Setup(x => x.Close(It.IsAny<bool>()));

            var vm = new CreateExpenseViewModel(dialogServiceMock.Object, expenseServiceMock.Object);
            vm.Model = this.editExpense;

            //act
            await vm.OnButtonSaveClickAsync();

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

            //act
            vm.OnButtonCancelClicked();

            //assert
            dialogServiceMock.Verify(x => x.Close(false));
        }
    }
}
