﻿using System;

namespace Stateless
{
    public partial class StateMachine<TState, TTrigger>
    {
        internal class DynamicTriggerBehaviour : TriggerBehaviour
        {
            readonly Func<object[], TState> _destination;
            internal Reflection.DynamicTransitionInfo TransitionInfo { get; private set; }

            public DynamicTriggerBehaviour(TTrigger trigger, Func<object[], TState> destination, 
                TransitionGuard transitionGuard, Reflection.DynamicTransitionInfo info)
                : base(trigger, transitionGuard)
            {
                if ((_destination = destination) == null)
                {
                    throw new ArgumentNullException(nameof(destination));
                }

                if ((TransitionInfo = info) == null)
                {
                    throw new ArgumentNullException(nameof(info));
                }
            }

            public override bool ResultsInTransitionFrom(TState source, object[] args, out TState destination)
            {
                destination = _destination(args);
                return true;
            }
        }
    }
}
