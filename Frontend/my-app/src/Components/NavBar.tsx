import { Button } from "./Button";

type NavbarProps = {
    User: string;
}

export function NavBar({User}: NavbarProps) {
    return(
        <>
            <p className="text-white">{User}</p>

            <Button>B1</Button>
            
            <Button>B2</Button>
            
            <Button>B3</Button>
            
            <Button>B4</Button>
        </>
    );
}