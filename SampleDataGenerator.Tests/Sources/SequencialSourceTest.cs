﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleDataGenerator.Sources;

namespace SampleDataGenerator.Tests.Sources
{
    [TestClass]
    public class SequentialSourceTest
    {
        [TestMethod]
        public void Get_must_return_one_element()
        {
            // arrange
            var count = 4;
            var range = Enumerable.Range(0, 2).ToArray();
            var generator = new SequencialSource<int>(range);

            // act
            var result = Enumerable.Range(0, count).Select(x => generator.Get());

            // assert
            Assert.AreEqual(count, result.Count());
        }

        [TestMethod]
        public void Get_must_return_requested_number_of_elements()
        {
            // arrange
            var count = 4;
            var range = Enumerable.Range(0, 2).ToArray();
            var generator = new SequencialSource<int>(range);

            // act
            var result = generator.Get(count);

            // assert
            Assert.AreEqual(count, result.Count());
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Constructor_must_throw_ArgumentNullException_for_null_source()
        {
            int[] range = null;

            var generator = new SequencialSource<int>(range);
        }

        [TestMethod]
        public void Get_must_return_null_if_source_is_empty()
        {
            // arrange
            var range = Enumerable.Empty<object>().ToArray();
            var generator = new SequencialSource<object>(range);

            // act
            var result = generator.Get();

            // assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Get_must_return_null_array_of_correct_length_if_source_is_empty()
        {
            // arrange
            var count = 4;
            var range = Enumerable.Empty<object>().ToArray();
            var generator = new SequencialSource<object>(range);

            // act
            var result = generator.Get(count);

            // assert
            Assert.IsTrue(result.All(x => x == null));
        }
    }
}