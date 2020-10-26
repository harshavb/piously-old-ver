using System.ComponentModel.DataAnnotations.Schema;
using osu.Framework.Input.Bindings;
using Piously.Game.Database;

namespace Piously.Game.Input.Bindings
{
    public class DatabasedKeyBinding : KeyBinding, IHasPrimaryKey
    {
        public int ID { get; set; }

        [Column("Keys")]
        public string KeysString
        {
            get => KeyCombination.ToString();
            private set => KeyCombination = value;
        }

        [Column("Action")]
        public int IntAction
        {
            get => (int)Action;
            set => Action = value;
        }
    }
}
