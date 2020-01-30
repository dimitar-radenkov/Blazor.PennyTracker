using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using PennyTracker.Web.Data;
using PennyTracker.Web.Services;
using PennyTracker.Web.ViewModels;

namespace PennyTracker.Web.Tests
{
    [TestClass]
    public class CreateExpenseViewModelTests
    {
        [TestMethod]
        public void WhenInitialzed_ShouldModelBeInstatiated()
        {
            //arrange
            var expenseServiceMock = new Mock<IExpenseService>();
            var dialogServiceMock = new Mock<IDialogService>();

            var vm = new CreateExpenseViewModel(dialogServiceMock.Object, expenseServiceMock.Object);
            vm.OnInitialized();

            //assert
            Assert.IsNotNull(vm.Model); 
            Assert.IsNotNull(vm.Model.SpentDate); 
        }

        [TestMethod]
        public void OnButtonSave_WhenInputIsCorrect_ShouldAddToExpenseService()
        {
            //arrange
            var expenseServiceMock = new Mock<IExpenseService>();
            expenseServiceMock.Setup(x => x.Add(It.IsAny<Expense>()));

            var dialogServiceMock = new Mock<IDialogService>();
            dialogServiceMock.Setup(x => x.Close(It.IsAny<bool>()));

            var vm = new CreateExpenseViewModel(dialogServiceMock.Object, expenseServiceMock.Object);
            vm.OnInitialized();

            //act
            vm.OnButtonSaveClicked();

            //assert
            expenseServiceMock.Verify(x => x.Add(vm.Model));
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
            vm.OnInitialized();

            //act
            vm.OnButtonCancelClicked();

            //assert
            dialogServiceMock.Verify(x => x.Close(false));
        }
    }
}
