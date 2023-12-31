module Models

open Giraffe
open Giraffe.ViewEngine
open LiteDB
open LiteDB.FSharp

[<CLIMutable>]
type Post =
    { id     : int
      title  : string
      content: string }

    member this.toHtml () = 
        div [] [
            h1 [ _id "title" ] [ rawText this.title ]
            rawText this.content
        ]

let example = {
    id      = 1
    title   = "Tesla Buccaneer: Charting the Electric Seas of Innovation"
    content = "
<h1>Avast, me hearties! Tesla Buccaneer: Charting the Electric Seas of Innovation</h1>

<p>Avast, me hearties! Gather 'round as we unveil a marvel of the modern age that even Blackbeard himself would give his left peg leg for – the <strong>Tesla Buccaneer</strong>, a ship of the land, if ye will. Prepare to be blown away as we chart the course through the seas of innovation and set our sights on the horizon of electric vehicular treasures!</p>

<h2>Unveilin' the Tesla Buccaneer: A Ride Fit for a Captain</h2>

<p>Avast, Tesla enthusiasts and landlubbers alike, feast yer eyes on the grandeur of the Tesla Buccaneer! With the sleek lines of a sloop and the speed of a swift brigantine, this electric vessel be settin' sail into uncharted territories. Captain Elon Musk, the mastermind behind this contraption, has once again proven that the seas of innovation be endless.</p>

<h2>Electric Thunder: The Power Behind the Tesla Buccaneer</h2>

<p>In true Tesla fashion, the heart of the Buccaneer lies beneath the hatch – a mighty electric motor that harnesses the power of the wind itself. Silent as the midnight tide, this vessel be gliding through the streets, leaving nary a footprint but the tracks of eco-friendly righteousness. The roar of the internal combustion engine be replaced by the hum of the Tesla Buccaneer's electric power – a symphony for the environmentally conscious corsair.</p>

<h2>Plunderin' the Range: How Far Can the Buccaneer Set Sail?</h2>

<p>Worried about bein' marooned with a dead battery? Fear not, me hearties! The Tesla Buccaneer boasts a range that would make even the most seasoned privateer raise a brow. With its mighty batteries and efficient navigation system, ye can plunder the highways for hundreds of nautical miles before needin' to dock for a recharge. Prepare to explore new lands without the worry of runnin' aground.</p>

<h2>Autopilot Magic: Let the Ship Steer Itself</h2>

<p>Avast, ye landlubbers! The Tesla Buccaneer be equipped with the mystical powers of autopilot. Navigate the busy seas of traffic without liftin' a finger – let the ship steer itself while ye ponder the mysteries of the seven continents. It be a revolution in transportation, settin' sail into a future where the captain's hands need not be shackled to the wheel.</p>

<h2>The Tesla Buccaneer's Treasure Chest: Interior Fit for a Captain</h2>

<p>Step aboard this land-faring vessel, and ye'll find an interior that would make Blackbeard himself green with envy. Plush seats fit for a captain, a navigational console that rivals the finest navigators of the Golden Age, and an entertainment system that would keep even the most disgruntled crew member entertained during the longest of journeys.</p>

<h2>Chartin' the Course: Pricing and Availability</h2>

<p>But what be the cost of such a fine ship, ye ask? Fear not, for Tesla has made the Buccaneer accessible to a wide range of treasure pursuers. Chart yer course to the nearest Tesla dealership to inquire about the pricing and availability. But beware, for such a vessel be in high demand, and the waiting list be longer than a sailor's yarn.</p>

<p>So, me hearties, prepare to set sail on the electric seas with the Tesla Buccaneer. It be a vessel that combines the spirit of adventure with the cutting-edge technology of the future. Buckle yer swashes and ready the anchor – the Tesla Buccaneer awaits those bold enough to seek the treasures of sustainable transportation.</p>

<p>Fair winds and a speedy charge to all ye future Tesla Buccaneers!</p>
"
}

let db    = new LiteDatabase ("posts.db", FSharpBsonMapper ())
let posts = db.GetCollection<Post> "posts"

let getById (id: int) =
    posts.FindById (BsonValue id)

let getPost () = 
    (getById 1).toHtml () |> htmlView
