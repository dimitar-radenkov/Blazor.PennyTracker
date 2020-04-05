using System;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using PennyTracker.BlazorServer.Events;
using PennyTracker.BlazorServer.Services;
using PennyTracker.BlazorServer.ViewModels;
using PennyTracker.Shared.Models;
using Prism.Events;

namespace PennyTracker.BlazorServer.Tests
{
    [TestClass]
    public class CreateExpenseViewModelTests
    {
        private Expense updateExpense;
        private Expense newExpense;

        [TestInitialize]
        public void Initialize()
        {
            this.updateExpense = new Expense
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
            var eventAggregatorMock = new Mock<IEventAggregator>();

            var vm = new CreateExpenseComponentViewModel(
                dialogServiceMock.Object, 
                expenseServiceMock.Object, 
                eventAggregatorMock.Object);

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
            var dialogServiceMock = new Mock<IDialogService>();
            var eventAggregatorMock = new Mock<IEventAggregator>();

            var vm = new CreateExpenseComponentViewModel(
                dialogServiceMock.Object,
                expenseServiceMock.Object,
                eventAggregatorMock.Object);
            vm.Model = this.updateExpense;

            //assert
            Assert.IsNotNull(vm.Model);
            Assert.AreEqual(this.updateExpense.Id, vm.Model.Id);
            Assert.AreEqual(this.updateExpense.Category, vm.Model.Category);
            Assert.AreEqual(this.updateExpense.CreationDate, vm.Model.CreationDate);
            Assert.AreEqual(this.updateExpense.SpentDate, vm.Model.SpentDate);
            Assert.AreEqual(this.updateExpense.Description, vm.Model.Description);
        }

        [TestMethod]
        public async Task OnButtonSave_WhenCreateNew_ShouldCallAddExpense()
        {
            //arrange
            var expenseServiceMock = new Mock<IExpenseService>();
            expenseServiceMock.Setup(x => x.AddAsync(
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<Category>(),
                It.IsAny<DateTime>()));

            var dialogServiceMock = new Mock<IDialogService>();
            dialogServiceMock.Setup(x => x.Close(It.IsAny<bool>()));

            var eventAggregatorMock = new Mock<IEventAggregator>();
            eventAggregatorMock.Setup(x => x.GetEvent<TransactionAddedEvent>().Publish(null));

            var vm = new CreateExpenseComponentViewModel(
                dialogServiceMock.Object, 
                expenseServiceMock.Object, 
                eventAggregatorMock.Object);
            vm.Model = this.newExpense;

            //act
            await vm.OnButtonSaveClickAsync();

            //assert
            expenseServiceMock.Verify(x => x.AddAsync(
                vm.Model.Description,
                vm.Model.Amount,
                vm.Model.Category,
                vm.Model.SpentDate));

            dialogServiceMock.Verify(x => x.Close(true));
        }

        [TestMethod]
        public async Task OnButtonSave_WhenEdit_ShouldCallUpdateExpense()
        {
            //arrange
            var expenseServiceMock = new Mock<IExpenseService>();
            expenseServiceMock.Setup(x => x.AddAsync(
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<Category>(),
                It.IsAny<DateTime>()));

            var dialogServiceMock = new Mock<IDialogService>();
            dialogServiceMock.Setup(x => x.Close(It.IsAny<bool>()));

            var eventAggregatorMock = new Mock<IEventAggregator>();

            var vm = new CreateExpenseComponentViewModel(
                dialogServiceMock.Object, 
                expenseServiceMock.Object,
                eventAggregatorMock.Object);
            vm.Model = this.updateExpense;

            //act
            await vm.OnButtonSaveClickAsync();

            //assert
            expenseServiceMock.Verify(x => x.UpdateAsync(
                vm.Model.Id,
                vm.Model.Description,
                vm.Model.Amount,
                vm.Model.Category,
                vm.Model.SpentDate));

            dialogServiceMock.Verify(x => x.Close(true));
        }

        [TestMethod]
        public void OnButtonCancel_ShouldCallDialogServiceClose()
        {
            //arrange
            var expenseServiceMock = new Mock<IExpenseService>();

            var dialogServiceMock = new Mock<IDialogService>();
            dialogServiceMock.Setup(x => x.Close(It.IsAny<bool>()));

            var eventAggregatorMock = new Mock<IEventAggregator>();

            var vm = new CreateExpenseComponentViewModel(
                dialogServiceMock.Object, 
                expenseServiceMock.Object, 
                eventAggregatorMock.Object);

            //act
            vm.OnButtonCancelClicked();

            //assert
            dialogServiceMock.Verify(x => x.Close(false));
        }
    }
}
