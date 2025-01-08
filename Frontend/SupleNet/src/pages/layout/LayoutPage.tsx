import { Outlet } from "react-router";
import { Navbar } from "./components/navbar/Navbar";
import { Toaster } from "sonner";
import { Footer } from "./components/footer/Footer";

export function LayoutPage()
{
    return(
        <div className="flex flex-col w-full min-h-screen gap-4">
            <Navbar></Navbar>
            <Toaster/>
            <main className="flex-grow">
                <Outlet></Outlet>
            </main>
            <Footer></Footer>
        </div>
    );
}