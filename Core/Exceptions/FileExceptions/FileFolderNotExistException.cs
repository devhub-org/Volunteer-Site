using Core.Resources;
using Provis.Core.Resources;
using System;
using System.Runtime.Serialization;

namespace Core.Exeptions.FileExceptions
{
    [Serializable]
    public class FileFolderNotExistException : FileException
    {
        public FileFolderNotExistException()
            : base(ErrorMessages.CannotSaveFile) { }

        public FileFolderNotExistException(Exception innerException)
            : base(ErrorMessages.CannotSaveFile, innerException) { }

        public FileFolderNotExistException(string path)
            : base(ErrorMessages.CannotSaveFile, path) { }

        protected FileFolderNotExistException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
