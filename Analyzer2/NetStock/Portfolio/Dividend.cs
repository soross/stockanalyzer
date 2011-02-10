using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetStock.Engine;

namespace DotNetStock.Portfolio
{
    class Dividend : Commentable
    {
        public Dividend(Stock stock, double amount, SimpleDate date)
        {
            this.stock = stock;
            this.amount = amount;
            this.date = date;
        }

        public Dividend(Dividend dividend)
        {
            this.stock = dividend.stock;
            this.amount = dividend.amount;
            this.date = dividend.date;
            this.comment = dividend.comment;
        }

        public Dividend setStock(Stock stock)
        {
            Dividend dividend = new Dividend(stock, this.getAmount(), this.getDate());
            dividend.setComment(this.comment);
            return dividend;
        }

        public Dividend setAmount(double amount)
        {
            Dividend dividend = new Dividend(this.getStock(), amount, this.getDate());
            dividend.setComment(this.comment);
            return dividend;
        }

        public Dividend setDate(SimpleDate date)
        {
            Dividend dividend = new Dividend(this.getStock(), this.getAmount(), date);
            dividend.setComment(this.comment);
            return dividend;
        }

        public void setComment(String comment)
        {
            this.comment = comment;
        }

        public String getComment()
        {
            return this.comment;
        }

        /**
         * @return the stock
         */
        public Stock getStock()
        {
            return stock;
        }

        /**
         * @return the amount
         */
        public double getAmount()
        {
            return amount;
        }

        /**
         * @return the date
         */
        public SimpleDate getDate()
        {
            return date;
        }

        private Stock stock;
        private double amount;
        private SimpleDate date;
        private String comment = "";
    }
}
