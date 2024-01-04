export function PostForm() {
    const [submitting, setSubmitting] = React.useState(false)
    
    function submit(event) {
        event.preventDefault()
        setSubmitting(true)
    }

    return (
        <div>
            <form onSubmit={submit} className="form-floating m-5">
                <input name="postTitle" value="postTitle" type="text" placeholder="Post Title"
                    className="form-control p-3 pt-5 m-3 w-75 fs-3" />
                <label for="postTitle" text="Post Title" />

                <input name="postContent" value="postContent" type="textarea" placeholder="Post Content"
                    className="form-control px-3 m-3" />
                <label for="postContent" text="Post Content" />

                <button type="submit" className="btn btn-primary float-end py-2 px-4 m-5" disabled={submitting}>
                    {submitting? "Submitting...": "Submit"}
                </button>
            </form>
        </div>
    )
}
