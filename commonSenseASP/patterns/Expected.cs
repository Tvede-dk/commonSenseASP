using System;
using System.Collections.Generic;
using System.Text;

namespace commonSenceASP.Patterns {
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
}
