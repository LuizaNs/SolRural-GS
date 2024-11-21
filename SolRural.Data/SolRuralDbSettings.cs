namespace SolRural.Data
{
    public class SolRuralDbSettings
    {
        public string ConnectionString { get; set; } 
        public string DatabaseName { get; set; } = null!;
        public string CultivoCollectionName { get; set; } = null!;
        public string FazendaCollectionName { get; set; } = null!;
        public string InstalacaoCollectionName { get; set; } = null!;
        public string LocalizacaoCollectionName { get; set; } = null!;
        public string MedicaoEnergCollectionName { get; set; } = null!;
        public string ProprietarioCollectionName { get; set; } = null!;
    }
}
