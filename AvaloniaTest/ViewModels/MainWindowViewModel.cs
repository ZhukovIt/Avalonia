using System;
using System.Reactive;
using AvaloniaTest.Models;
using ReactiveUI;

namespace AvaloniaTest.ViewModels
{
    public sealed class MainWindowViewModel : ReactiveObject
    {
        private double m_firstValue;
        private double m_secondValue;
        private Operation m_operation = Operation.Add;

        public double ShownValue
        {
            get => m_secondValue;
            set => this.RaiseAndSetIfChanged(ref m_secondValue, value);
        }

        public ReactiveCommand<int, Unit> AddNumberCommand { get; }
        public ReactiveCommand<Unit, Unit> RemoveLastNumberCommand { get; }
        public ReactiveCommand<Operation, Unit> ExecuteOperationCommand { get; }

        public MainWindowViewModel()
        {
            AddNumberCommand = ReactiveCommand.Create<int>(AddNumber);
            ExecuteOperationCommand = ReactiveCommand.Create<Operation>(ExecuteOperation);
            RemoveLastNumberCommand = ReactiveCommand.Create(RemoveLastNumber);
        }

        private void AddNumber(int value)
        {
            ShownValue = ShownValue * 10 + value;
        }

        public void ClearScreen()
        {
            ShownValue = 0;
            m_operation = Operation.Add;
            m_firstValue = 0;
        }

        public void Exit()
        {
            Environment.Exit(0);
        }

        public void RemoveLastNumber()
        {
            ShownValue = (int)ShownValue / 10;
        }

        private void ExecuteOperation(Operation operation)
        {
            switch (m_operation)
            {
                case Operation.Add:
                {
                    m_firstValue += m_secondValue;
                    ShownValue = 0;
                    break;
                }

                case Operation.Subtract:
                {
                    m_firstValue -= m_secondValue;
                    ShownValue = 0;
                    break;
                }

                case Operation.Multiply:
                {
                    m_firstValue *= m_secondValue;
                    ShownValue = 0;
                    break;
                }

                case Operation.Divide:
                {
                    m_firstValue /= m_secondValue;
                    ShownValue = 0;
                    break;
                }
            }

            if (operation == Operation.Result)
            {
                ShownValue = m_firstValue;
                m_operation = Operation.Add;
                m_firstValue = 0;
            }
            else
            {
                m_operation = operation;
            }
        }
    }
}