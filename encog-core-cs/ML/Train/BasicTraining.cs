//
// Encog(tm) Core v3.0 - .Net Version
// http://www.heatonresearch.com/encog/
//
// Copyright 2008-2011 Heaton Research, Inc.
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
using System.Collections.Generic;
using Encog.ML.Data;
using Encog.ML.Train.Strategy;
using Encog.ML.Train.Strategy.End;
using Encog.Neural.Networks.Training.Propagation;

namespace Encog.ML.Train
{
    /// <summary>
    /// An abstract class that implements basic training for most training
    /// algorithms. Specifically training strategies can be added to enhance the
    /// training.
    /// </summary>
    ///
    public abstract class BasicTraining : MLTrain
    {
        private readonly TrainingImplementationType implementationType;

        /// <summary>
        /// The training strategies to use.
        /// </summary>
        ///
        private readonly IList<IStrategy> strategies;

        /// <summary>
        /// The current iteration.
        /// </summary>
        ///
        private int iteration;

        /// <summary>
        /// Construct the object, specify the implementation type.
        /// </summary>
        /// <param name="implementationType_0"></param>
        public BasicTraining(TrainingImplementationType implementationType_0)
        {
            strategies = new List<IStrategy>();
            implementationType = implementationType_0;
        }

        #region MLTrain Members

        /// <summary>
        /// Training strategies can be added to improve the training results. There
        /// are a number to choose from, and several can be used at once.
        /// </summary>
        ///
        /// <param name="strategy">The strategy to add.</param>
        public virtual void AddStrategy(IStrategy strategy)
        {
            strategy.Init(this);
            strategies.Add(strategy);
        }

        /// <summary>
        /// Should be called after training has completed and the iteration method
        /// will not be called any further.
        /// </summary>
        ///
        public virtual void FinishTraining()
        {
        }


        /// <inheritdoc/>
        public virtual double Error { get; set; }


        /// <value>the iteration to set</value>
        public virtual int IterationNumber
        {
            get { return iteration; }
            set { iteration = value; }
        }


        /// <value>The strategies to use.</value>
        public virtual IList<IStrategy> Strategies
        {
            get { return strategies; }
        }


        /// <summary>
        /// Set the training object that this strategy is working with.
        /// </summary>
        public virtual MLDataSet Training { get; set; }


        /// <value>True if training can progress no further.</value>
        public virtual bool TrainingDone
        {
            get
            {
                foreach (IStrategy strategy  in  strategies)
                {
                    if (strategy is EndTrainingStrategy)
                    {
                        var end = (EndTrainingStrategy) strategy;
                        if (end.ShouldStop())
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }


        /// <summary>
        /// Perform the specified number of training iterations. This is a basic
        /// implementation that just calls iteration the specified number of times.
        /// However, some training methods, particularly with the GPU, benefit
        /// greatly by calling with higher numbers than 1.
        /// </summary>
        ///
        /// <param name="count">The number of training iterations.</param>
        public virtual void Iteration(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Iteration();
            }
        }

        /// <summary>
        /// The implementation type.
        /// </summary>
        public virtual TrainingImplementationType ImplementationType
        {
            get { return implementationType; }
        }


        /// <summary>
        /// from Encog.ml.train.MLTrain
        /// </summary>
        ///
        public abstract bool CanContinue { get; }

        /// <summary>
        /// from Encog.ml.train.MLTrain
        /// </summary>
        ///
        public abstract MLMethod Method {
            get; }


        /// <summary>
        /// from Encog.ml.train.MLTrain
        /// </summary>
        ///
        public abstract void Iteration();

        /// <summary>
        /// from Encog.ml.train.MLTrain
        /// </summary>
        ///
        public abstract TrainingContinuation Pause();

        /// <summary>
        /// from Encog.ml.train.MLTrain
        /// </summary>
        ///
        public abstract void Resume(
            TrainingContinuation state);

        #endregion

        /// <summary>
        /// Call the strategies after an iteration.
        /// </summary>
        ///
        public void PostIteration()
        {
            foreach (IStrategy strategy  in  strategies)
            {
                strategy.PostIteration();
            }
        }

        /// <summary>
        /// Call the strategies before an iteration.
        /// </summary>
        ///
        public void PreIteration()
        {
            iteration++;


            foreach (IStrategy strategy  in  strategies)
            {
                strategy.PreIteration();
            }
        }
    }
}
