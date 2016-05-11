using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace commonSenseASP.Patterns {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Expected<T> {
        private Exception e;
        private T value;

        public Expected(Exception e) {
            this.e = e;
        }

        public Expected(T value) {
            this.value = value;
        }

        public bool IsExpected() {
            return e == null && value != null;
        }
        public bool IsError() {
            return e != null;
        }

        public Exception GetError() {
            return e;
        }

        public T GetValue() {
            if (e != null) {
                throw e;
            }
            return value;
        }

        public static Expected<T> Success(T value) {
            return new Expected<T>(value);
        }

        public static Expected<T> Failed(Exception e) {
            return new Expected<T>(e);
        }
    }
    public static class ExpectedFactory {

        public static Expected<T> Success<T>(T value) {
            return Expected<T>.Success(value);
        }

        public static Task<Expected<T>> TaskSuccess<T>(T value) {
            return Task.FromResult(Success<T>(value));
        }

        public static Expected<T> Error<T>(string message) {
            return Expected<T>.Failed(new ArgumentException(message));
        }

        public static Expected<T> Error<T>(Exception exception) {
            return Expected<T>.Failed(exception);
        }

        public static Task<Expected<T>> TaskError<T>(string message) {
            return Task.FromResult(Error<T>(message));
        }

        public static Task<Expected<T>> TaskError<T>(Exception exception) {
            return Task.FromResult(Error<T>(exception));
        }
    }
}
