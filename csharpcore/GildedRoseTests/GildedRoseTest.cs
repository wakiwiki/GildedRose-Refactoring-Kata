﻿using Xunit;
using System.Collections.Generic;
using FluentAssertions;
using GildedRoseKata;

namespace GildedRoseTests
{
    public class GildedRoseTest
    {
        [Fact]
        public void foo()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Items[0].Name.Should().Be("foo");
            //Assert.Equal("foo", Items[0].Name);
        }

        [Fact]
        public void degrade_quality_once_when_one_day_passed()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "normal item", SellIn = 2, Quality = 4 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Items[0].Quality.Should().Be(3);
        }

        [Fact]
        public void degrade_quality_twice_as_fast_when_sell_by_date_has_passed()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "normal item", SellIn = -1, Quality = 4 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Items[0].Quality.Should().Be(2);
        }

        [Fact]
        public void degrade_quality_should_keep_it_positive()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "normal item", SellIn = 1, Quality = 1 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Items[0].Quality.Should().BeGreaterThanOrEqualTo(0);
        }

        [Fact]
        public void increase_quality_when_aged_brie_gets_older()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 2, Quality = 4 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Items[0].Quality.Should().BeGreaterThan(4);
            Items[0].Quality.Should().Be(5);
        }


        [Fact]
        public void aged_brie_quality_should_be_increased_twice__when_sell_by_date_has_passed()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = -1, Quality = 48 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Items[0].Quality.Should().Be(50);
        }

        [Fact]
        public void increase_quality_never_sets_more_than_fifty()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 2, Quality = 50 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Items[0].Quality.Should().BeLessThanOrEqualTo(50);
            Items[0].Quality.Should().Be(50);
        }

        [Fact]
        public void sulfuras_sellin_should_not_be_matter()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 2, Quality = 80 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Items[0].SellIn.Should().Be(2);
        }


        [Fact]
        public void sulfuras_quality_must_be_eighty()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 2, Quality = 80 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Items[0].Quality.Should().Be(80);
        }

        [Theory()]
        [InlineData(11)]
        [InlineData(12)]
        public void backstage_pass_quality_should_be_increased_once_before_sellin_gets_closer(int sellin)
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellin, Quality = 30 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Items[0].Quality.Should().Be(31);
        }

        [Theory()]
        [InlineData(10)]
        [InlineData(8)]
        [InlineData(6)]
        public void backstage_pass_quality_should_be_increased_by_two_when_there_are_ten_days_or_less_for_sellin(int sellin)
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellin, Quality = 30 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Items[0].Quality.Should().Be(32);
        }


        [Theory()]
        [InlineData(5)]
        [InlineData(3)]
        [InlineData(1)]
        public void backstage_pass_quality_should_be_increased_by_three_when_there_are_five_days_or_less_for_sellin(int sellin)
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellin, Quality = 30 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Items[0].Quality.Should().Be(33);
        }

        [Fact]
        public void backstage_pass_quality_should_be_set_to_zero_when_sell_by_date_has_passed()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 30 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Items[0].Quality.Should().Be(0);
        }


        [Fact]
        public void conjured_item_quality_degrade_twice_as_fast_as_normal_item()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 2, Quality = 20 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Items[0].Quality.Should().Be(18);
        }

    }
}
