using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using osu.Framework.Platform;
using Piously.Game.Input.Bindings;
using osu.Framework.Input.Bindings;

namespace Piously.Game.Input
{
    public class KeyBindingStore
    {
        public event Action KeyBindingChanged;

        protected List<KeyBinding> keyBindings = new List<KeyBinding>();

        protected readonly Storage Storage;

        public KeyBindingStore(Storage storage = null)
        {
            Storage = storage;
        }

        public void Register(PiouslyKeyBindingContainer manager) => insertDefaults(manager.DefaultKeyBindings);

        /// <summary>
        /// Retrieve all user-defined key combinations (in a format that can be displayed) for a specific action.
        /// </summary>
        /// <param name="globalAction">The action to lookup.</param>
        /// <returns>A set of display strings for all the user's key configuration for the action.</returns>
        public IEnumerable<string> GetReadableKeyCombinationsFor(GlobalAction globalAction)
        {
            foreach (var action in Query().Where(b => (GlobalAction)b.Action == globalAction))
            {
                string str = action.KeyCombination.ReadableString();

                // even if found, the readable string may be empty for an unbound action.
                if (str.Length > 0)
                    yield return str;
            }
        }

        private void insertDefaults(IEnumerable<KeyBinding> defaults)
        {
            foreach (var group in defaults.GroupBy(k => k.Action))
            {
                int count = Query().Count(k => (int)k.Action == (int)group.Key);
                int aimCount = group.Count();

                if (aimCount <= count)
                    return;

                foreach(var insertable in group.Skip(count).Take(aimCount - count))
                {
                    Update(new KeyBinding(insertable.KeyCombination, insertable.Action));
                }
            }
        }

        /// <summary>
        /// Retrieve <see cref="DatabasedKeyBinding"/>s.
        /// </summary>
        /// <returns></returns>
        public List<KeyBinding> Query() => keyBindings;

        public void Update(KeyBinding keyBinding)
        {
            foreach(var group in keyBindings.GroupBy(k => k.Action))
            {
                if(Query().Count(k => k.Action == keyBinding.Action) > 0)
                {
                    if(!(Query().Count(k => k.KeyCombination.Equals(keyBinding.KeyCombination)) > 0))
                    {
                        int value = keyBindings.FindIndex(k => k.KeyCombination.Equals(keyBinding.KeyCombination));
                        keyBindings.RemoveAt(value);
                        keyBindings.Insert(value, keyBinding);
                    }
                }
            }
            //WRITE TO FILE
            KeyBindingChanged?.Invoke();

            Stream file = Storage.GetStream("keybinds.ini", FileAccess.ReadWrite, FileMode.OpenOrCreate);

            if (file.CanWrite && file.CanRead)
            {
                bool foundKeyBindingInFile = false;
                string actionString = "";

                switch (keyBinding.Action)
                {
                    case GlobalAction.Select:
                        actionString = "Select";
                        break;
                    case GlobalAction.Back:
                        actionString = "Back";
                        break;
                    case GlobalAction.ToggleSettings:
                        actionString = "ToggleSettings";
                        break;
                }

                if (file.Length > 0)
                {
                    byte[] buffer = new byte[file.Length + 10]; // + 10 is for padding
                    int bytesToRead = (int)file.Length;
                    int bytesRead = 0;
                    do {
                        int n = file.Read(buffer, bytesRead, 10); // Read 10 bytes at a time
                        bytesRead += n;
                        bytesToRead -= n;
                    } while (bytesToRead > 0);

                    string[] fileContent = buffer.ToString().Split("\n");
                    for(int i = 0; i < fileContent.Length; i++)
                    {
                        string line = fileContent[i];
                        if (line.IndexOf(actionString) != -1)
                        {
                            foundKeyBindingInFile = true;
                            if (keyBinding.KeyCombination.Keys.Length == 1)
                            {
                                line = actionString + " = " + keyBinding.KeyCombination.Keys[0].ToString() + "\n";
                            }
                            else
                            {
                                string keyCombinationString = keyBinding.KeyCombination.Keys[0].ToString();
                                foreach (InputKey key in keyBinding.KeyCombination.Keys)
                                {
                                    keyCombinationString += " + " + keyBinding.KeyCombination.Keys[1].ToString();
                                }

                                line = actionString + " = " + keyCombinationString + "\n";
                            }

                            file.SetLength(0);
                            string writeOut = String.Join("\n", fileContent);
                            file.Write(Encoding.ASCII.GetBytes(writeOut));
                        }
                    }
                }
                if(file.Length == 0 || !foundKeyBindingInFile)
                {
                    string writeOut;
                    if (keyBinding.KeyCombination.Keys.Length == 1)
                    {
                        writeOut = actionString + " = " + keyBinding.KeyCombination.Keys[0].ToString() + "\n";
                    }
                    else
                    {
                        string keyCombinationString = keyBinding.KeyCombination.Keys[0].ToString();
                        foreach (InputKey key in keyBinding.KeyCombination.Keys)
                        {
                            keyCombinationString += " + " + keyBinding.KeyCombination.Keys[1].ToString();
                        }

                        writeOut = actionString + " = " + keyCombinationString + "\n";
                    }

                    file.Write(Encoding.ASCII.GetBytes(writeOut));
                }

                file.Close();
            }
            else
            {
                throw new FileLoadException("Unable to open keybinding file; insufficient permissions (WriteAccess = " + file.CanWrite + ", ReadAccess = " + file.CanRead + ")");
            }
        }
    }
}
