﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using FemDesign.Calculate;
using System.ComponentModel;

namespace FemDesign.Results.Utils
{
    public static class UtilResultMethods
    {

        /// <summary>
        /// Filter the result list of type T by the name of the specified load combination.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <param name="propertyName">Type T property name related to load combination.</param>
        /// <param name="loadCombination">Load combination name to filter results.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<T> FilterResultsByLoadCombination<T>(this List<T> results, string propertyName, string loadCombination) where T : IResult
        {
            PropertyInfo property = typeof(T).GetProperty(propertyName);
            if (property == null)
            {
                throw new ArgumentException($"Porperty {property} doesn't exist in type {typeof(T).Name}.");
            }

            if (!results.Select(r => property.GetValue(r).ToString()).Contains(loadCombination, StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException($"Incorrect or unknown load combination name: {loadCombination}.");
            }
            var filteredResults = results.Where(r => String.Equals(property.GetValue(r).ToString(), loadCombination, StringComparison.OrdinalIgnoreCase)).ToList();

            return filteredResults;
        }

        /// <summary>
        /// Filter the result list of type T by the names of the specified load combinations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <param name="propertyName">Type T property name related to load combinations.</param>
        /// <param name="loadCombination">List of load combination names to filter results.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<T> FilterResultsByLoadCombination<T>(this List<T> results, string propertyName, List<string> loadCombination) where T : IResult
        {
            PropertyInfo property = typeof(T).GetProperty(propertyName);
            if (property == null)
            {
                throw new ArgumentException($"Porperty {property} doesn't exist in type {typeof(T).Name}.");
            }

            List<T> filteredResults = new List<T>();
            foreach (var comb in loadCombination)
            {
                if (!results.Select(r => property.GetValue(r).ToString()).Contains(comb, StringComparer.OrdinalIgnoreCase))
                {
                    throw new ArgumentException($"Incorrect or unknown load combination name: {comb}.");
                }
                var res = results.Where(r => String.Equals(property.GetValue(r).ToString(), comb, StringComparison.OrdinalIgnoreCase)).ToList();
                
                filteredResults.AddRange(res);
            }

            return filteredResults;
        }

        /// <summary>
        /// Filter the result list of type T by the index of the specified shape identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <param name="propertyName">Type T property name related to shape identifier.</param>
        /// <param name="shapeId">Index of shape identifier to filter results.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<T> FilterResultsByShapeId<T>(this List<T> results, string propertyName, int shapeId) where T : IResult
        {
            PropertyInfo property = typeof(T).GetProperty(propertyName);
            if (property == null)
            {
                throw new ArgumentException($"Property {property} doesn't exist in type {typeof(T).Name}.");
            }

            if ((shapeId < 1) || (shapeId > (int)results.Select(r => property.GetValue(r)).Max()))
            {
                throw new ArgumentException($"ShapeId {shapeId} is out of range.");
            }
            var filteredResults = results.Where(r => (int)property.GetValue(r) == shapeId).ToList();

            return filteredResults;
        }

        /// <summary>
        /// Filter the result list of type T by the indexes of the specified shape identifiers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <param name="propertyName">Type T property name related to shape identifiers.</param>
        /// <param name="shapeId">Indexes of shape identifiers to filter results.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<T> FilterResultsByShapeId<T>(this List<T> results, string propertyName, List<int> shapeId) where T : IResult
        {
            PropertyInfo property = typeof(T).GetProperty(propertyName);
            if (property == null)
            {
                throw new ArgumentException($"Property {property} doesn't exist in type {typeof(T).Name}.");
            }

            List<T> filteredResults = new List<T>();
            foreach (var shape in shapeId)
            {
                if ((shape < 1) || (shape > (int)results.Select(r => property.GetValue(r)).Max()))
                {
                    throw new ArgumentException($"ShapeId {shape} is out of range.");
                }
                var res = results.Where(r => (int)property.GetValue(r) == shape).ToList();

                filteredResults.AddRange(res);
            }

            return filteredResults;
        }
    }
}
