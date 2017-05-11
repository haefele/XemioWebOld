using System;
using Newtonsoft.Json.Linq;

namespace Xemio.Api.Data.Models.Notes
{
    public class UpdateFolder : JObject
    {
        public bool HasName()
        {
            return this.TryGetValue(nameof(this.Name), StringComparison.OrdinalIgnoreCase, out var _);
        }
        public string Name
        {
            get
            {
                this.TryGetValue(nameof(this.Name), StringComparison.OrdinalIgnoreCase, out var nameToken);
                return nameToken.ToObject<string>();
            }
            set
            {
                this[nameof(this.Name)] = value;
            }
        }
        
        public bool HasParentFolderId()
        {
            return this.TryGetValue(nameof(this.ParentFolderId), StringComparison.OrdinalIgnoreCase, out var _);
        }
        public long? ParentFolderId
        {
            get
            {
                this.TryGetValue(nameof(this.ParentFolderId), StringComparison.OrdinalIgnoreCase, out var parentFolderIdToken);
                return parentFolderIdToken.ToObject<long?>();
            }
            set
            {
                this[nameof(this.ParentFolderId)] = value;
            }
        }
    }
}