using System;
using System.Text;

namespace GetRelatedPostsByTag.Utility
{
  public interface IExceptionLogFormatter
  {
    string FormatExceptionLogMessage(Exception ex, StringBuilder builder = null);
  }
}