namespace UniversityTool.ViewModels.Base
{
    internal abstract class TitledViewModel : BaseViewModel
    {
        private string? _windowTitle;

        public string WindowTitle
        {
            get => _windowTitle;
            set => Set(ref _windowTitle, value);
        }
    }
}
