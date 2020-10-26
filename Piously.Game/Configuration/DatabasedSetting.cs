using System.ComponentModel.DataAnnotations.Schema;
using Piously.Game.Database;

namespace Piously.Game.Configuration
{
    [Table("Settings")]
    public class DatabasedSetting : IHasPrimaryKey
    {
        public int ID { get; set; }

        [Column("Key")]
        public string Key { get; set; }

        [Column("Value")]
        public string StringValue
        {
            get => Value.ToString();
            set => Value = value;
        }

        public object Value;

        public DatabasedSetting(string key, object value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Constructor for derived classes that may require serialisation.
        /// </summary>
        public DatabasedSetting()
        {
        }

        public override string ToString() => $"{Key}=>{Value}";
    }
}
