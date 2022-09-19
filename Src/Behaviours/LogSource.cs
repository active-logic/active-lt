using Active.Core.Details;

namespace Active.Core{
public interface LogSource{

    History GetHistory ();
    string  GetLog     ();
    bool    IsLogging  ();

}}
