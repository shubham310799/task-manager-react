export default function StickyStackTabs() {
    const tabs = [
        { id: 1, title: "Football" },
        { id: 2, title: "Cricket" },
        { id: 3, title: "Basketball" },
        { id: 4, title: "Tennis" },
        { id: 5, title: "Hockey" },
    ];

    return (
        <div className="bg-black min-h-screen p-10">
            <div className="max-w-3xl mx-auto space-y-6">
                {tabs.map((tab, index) => (
                    <div
                        key={tab.id}
                        className="sticky rounded-3xl overflow-hidden shadow-2xl"
                        style={{
                            top: `${index * 70}px`,
                            zIndex: 50 + index,
                        }}
                    >
                        <div className="bg-zinc-900 text-white p-10 h-[300px] border border-zinc-700">
                            <h2 className="text-4xl font-bold mb-4">{tab.title}</h2>

                            <p className="text-zinc-300">
                                Scroll to see sticky stacking effect.
                            </p>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
}