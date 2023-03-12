namespace UniversityTool.ViewModels.Base
{
    internal abstract class TitledViewModel : BaseViewModel
    {
        private string? _title;

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
    }
}
