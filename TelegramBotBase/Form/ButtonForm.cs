﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotBase.Form
{
    public class ButtonForm
    {
        List<List<ButtonBase>> Buttons = new List<List<ButtonBase>>();

        public ButtonForm()
        {

        }

        public void AddButtonRow(List<ButtonBase> row)
        {
            Buttons.Add(row);
        }

        public void AddButtonRow(params ButtonBase[] row)
        {
            AddButtonRow(row.ToList());
        }

        public static T[][] SplitTo<T>(IEnumerable<T> items, int itemsPerRow = 2)
        {
            T[][] splitted = default(T[][]);

            try
            {
                var t = items.Select((a, index) => new { a, index })
                             .GroupBy(a => a.index / itemsPerRow)
                             .Select(a => a.Select(b => b.a).ToArray()).ToArray();

                splitted = t;
            }
            catch
            {

            }

            return splitted;
        }

        /// <summary>
        /// Fügt die Buttons automatisch aufgesplittet in Spalten ein.
        /// </summary>
        /// <param name="buttons"></param>
        /// <param name="buttonsPerRow"></param>
        public void AddSplitted(IEnumerable<ButtonBase> buttons, int buttonsPerRow = 2)
        {
            var sp = SplitTo<ButtonBase>(buttons, buttonsPerRow);

            foreach(var bl in sp)
            {
                AddButtonRow(bl);
            }
        }

        public InlineKeyboardButton[][] ToArray()
        {
            var ikb = this.Buttons.Select(a => a.Select(b => InlineKeyboardButton.WithCallbackData(b.Text, b.Value)).ToArray()).ToArray();

            return ikb;
        }

        public static implicit operator InlineKeyboardMarkup(ButtonForm form)
        {
            InlineKeyboardMarkup ikm = new InlineKeyboardMarkup();

            ikm.InlineKeyboard = form.ToArray();

            return ikm;
        }
    }
}