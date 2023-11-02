using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Domain.Errors;

public static partial class Errors
{
    public static class Contact
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "Contact.DuplicateEmail",
            description: "Contact with this email already exists!");

        public static Error NotFound => Error.NotFound(
            code: "Contact.NotFound", 
            description:"Contact not found!");
    }
}
