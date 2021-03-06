//
// Encog(tm) Core v3.3 - .Net Version
// http://www.heatonresearch.com/encog/
//
// Copyright 2008-2014 Heaton Research, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//   
// For more information on Heaton Research copyrights, licenses 
// and trademarks visit:
// http://www.heatonresearch.com/copyright
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Encog.Neural.Networks.Training;

namespace Encog.ML.Fitness
{
    /// <summary>
    /// A fitness objective.
    /// </summary>
    public class FitnessObjective
    {
        /// <summary>
        /// The weight.
        /// </summary>
        private double weight;

        /// <summary>
        /// The score function.
        /// </summary>
        private ICalculateScore score;

        /// <summary>
        /// Construct the fitness objective. 
        /// </summary>
        /// <param name="weight">The weight.</param>
        /// <param name="score">The score.</param>
        public FitnessObjective(double weight, ICalculateScore score)
        {
            this.weight = weight;
            this.score = score;
        }

        /// <summary>
        /// The weight.
        /// </summary>
        public double Weight
        {
            get
            {
                return weight;
            }
        }

        /// <summary>
        /// The score.
        /// </summary>
        public ICalculateScore Score
        {
            get
            {
                return score;
            }
        }
    }
}
