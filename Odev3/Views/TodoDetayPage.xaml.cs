using Odev3.Models;
using Odev3.ViewModels;

namespace Odev3.Views;

public partial class TodoDetayPage : ContentPage
{
    private readonly TodoDetayViewModel _viewModel;

    public TodoDetayPage(TodoDetayViewModel vm, Gorev gorevToEdit = null)
    {
        InitializeComponent();
        _viewModel = vm;
        BindingContext = _viewModel;

        if (gorevToEdit != null)
        {
            _viewModel.Initialize(gorevToEdit);
        }
    }
}