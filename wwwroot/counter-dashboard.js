// Counter Dashboard Page 3 - JavaScript

class CounterDashboard {
    constructor() {
        this.lineAData = {
            model: 'ESMI000TGG',
            counter: 1200,
            targetPlan: -1300,
            difference: -1300,
            processStop: 0,
            machineStop: 0
        };

        this.lineBData = {
            model: 'EST80MWPK',
            counter: 1200,
            targetPlan: -1300,
            difference: -1300,
            processStop: 0,
            machineStop: 0
        };

        this.init();
    }

    init() {
        this.renderCounterDashboard();
        this.startDataUpdate();
    }

    renderCounterDashboard() {
        const page3Container = document.querySelector('.carousel-page:nth-child(3)');
        if (!page3Container) return;

        page3Container.innerHTML = `
            <div class="counter-dashboard-container">
                <div class="counter-header">
                    <div class="counter-header-left">
                        <div class="counter-logo">
                            <img src="images/sharp-logo.png" alt="SHARP Logo">
                        </div>
                    </div>
                </div>

                <div class="counter-lines-container">
                    <!-- LINE A -->
                    <div class="counter-line-section">
                        <div class="counter-line-header">
                            <div>
                                <h2 class="counter-line-title">COUNTER LINE A</h2>
                                <h3 class="counter-line-model">${this.lineAData.model}</h3>
                            </div>
                        </div>

                        <div class="counter-cards-grid">
                            <!-- Main Counter -->
                            <div class="counter-main-card">
                                <div class="counter-main-number" id="lineACounter">${this.lineAData.counter}</div>
                                <div class="counter-main-label">Unit</div>
                            </div>

                            <!-- Target Plan -->
                            <div class="counter-secondary-card">
                                <div class="counter-secondary-label">TARGET PLAN</div>
                                <div class="counter-secondary-number" id="lineATargetPlan">${this.lineAData.targetPlan}</div>
                                <div class="counter-secondary-badge">Unit</div>
                            </div>

                            <!-- Difference -->
                            <div class="counter-secondary-card">
                                <div class="counter-secondary-label">DIFFERENCE</div>
                                <div class="counter-secondary-number" id="lineADifference">${this.lineAData.difference}</div>
                                <div class="counter-secondary-badge">Unit</div>
                            </div>
                        </div>

                        <!-- Bottom Row -->
                        <div class="counter-bottom-row">
                            <!-- Process Stop -->
                            <div class="counter-bottom-card">
                                <div class="counter-bottom-label">PROCESS STOP</div>
                                <div class="counter-bottom-number" id="lineAProcessStop">${this.lineAData.processStop}</div>
                                <div class="counter-bottom-badge">Min</div>
                            </div>

                            <!-- Machine Stop -->
                            <div class="counter-bottom-card">
                                <div class="counter-bottom-label">MACHINE STOP</div>
                                <div class="counter-bottom-number" id="lineAMachineStop">${this.lineAData.machineStop}</div>
                                <div class="counter-bottom-badge">Min</div>
                            </div>

                            <!-- Placeholder cards -->
                            <div class="counter-bottom-card">
                                <div class="counter-bottom-label">EFFICIENCY</div>
                                <div class="counter-bottom-number">85</div>
                                <div class="counter-bottom-badge">%</div>
                            </div>

                            <div class="counter-bottom-card">
                                <div class="counter-bottom-label">QUALITY</div>
                                <div class="counter-bottom-number">98</div>
                                <div class="counter-bottom-badge">%</div>
                            </div>
                        </div>
                    </div>

                    <!-- LINE B -->
                    <div class="counter-line-section">
                        <div class="counter-line-header">
                            <div>
                                <h2 class="counter-line-title">COUNTER LINE B</h2>
                                <h3 class="counter-line-model">${this.lineBData.model}</h3>
                            </div>
                        </div>

                        <div class="counter-cards-grid">
                            <!-- Main Counter -->
                            <div class="counter-main-card">
                                <div class="counter-main-number" id="lineBCounter">${this.lineBData.counter}</div>
                                <div class="counter-main-label">Unit</div>
                            </div>

                            <!-- Target Plan -->
                            <div class="counter-secondary-card">
                                <div class="counter-secondary-label">TARGET PLAN</div>
                                <div class="counter-secondary-number" id="lineBTargetPlan">${this.lineBData.targetPlan}</div>
                                <div class="counter-secondary-badge">Unit</div>
                            </div>

                            <!-- Difference -->
                            <div class="counter-secondary-card">
                                <div class="counter-secondary-label">DIFFERENCE</div>
                                <div class="counter-secondary-number" id="lineBDifference">${this.lineBData.difference}</div>
                                <div class="counter-secondary-badge">Unit</div>
                            </div>
                        </div>

                        <!-- Bottom Row -->
                        <div class="counter-bottom-row">
                            <!-- Process Stop -->
                            <div class="counter-bottom-card">
                                <div class="counter-bottom-label">PROCESS STOP</div>
                                <div class="counter-bottom-number" id="lineBProcessStop">${this.lineBData.processStop}</div>
                                <div class="counter-bottom-badge">Min</div>
                            </div>

                            <!-- Machine Stop -->
                            <div class="counter-bottom-card">
                                <div class="counter-bottom-label">MACHINE STOP</div>
                                <div class="counter-bottom-number" id="lineBMachineStop">${this.lineBData.machineStop}</div>
                                <div class="counter-bottom-badge">Min</div>
                            </div>

                            <!-- Placeholder cards -->
                            <div class="counter-bottom-card">
                                <div class="counter-bottom-label">EFFICIENCY</div>
                                <div class="counter-bottom-number">87</div>
                                <div class="counter-bottom-badge">%</div>
                            </div>

                            <div class="counter-bottom-card">
                                <div class="counter-bottom-label">QUALITY</div>
                                <div class="counter-bottom-number">96</div>
                                <div class="counter-bottom-badge">%</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        `;
    }

    updateProcessStopData(lineA, lineB) {
        // Update Line A Process Stop
        this.lineAData.processStop = lineA.processStop || 0;
        this.lineAData.machineStop = lineA.machineStop || 0;

        // Update Line B Process Stop
        this.lineBData.processStop = lineB.processStop || 0;
        this.lineBData.machineStop = lineB.machineStop || 0;

        // Update DOM elements
        this.updateCounterElements();
    }

    updateCounterElements() {
        // Line A
        const lineAProcessStopEl = document.getElementById('lineAProcessStop');
        const lineAMachineStopEl = document.getElementById('lineAMachineStop');
        
        if (lineAProcessStopEl) lineAProcessStopEl.textContent = this.lineAData.processStop;
        if (lineAMachineStopEl) lineAMachineStopEl.textContent = this.lineAData.machineStop;

        // Line B
        const lineBProcessStopEl = document.getElementById('lineBProcessStop');
        const lineBMachineStopEl = document.getElementById('lineBMachineStop');
        
        if (lineBProcessStopEl) lineBProcessStopEl.textContent = this.lineBData.processStop;
        if (lineBMachineStopEl) lineBMachineStopEl.textContent = this.lineBData.machineStop;

        // Update counter values
        const lineACounterEl = document.getElementById('lineACounter');
        const lineBCounterEl = document.getElementById('lineBCounter');
        
        if (lineACounterEl) lineACounterEl.textContent = this.lineAData.counter;
        if (lineBCounterEl) lineBCounterEl.textContent = this.lineBData.counter;
    }

    startDataUpdate() {
        // Update data setiap 5 detik
        setInterval(() => {
            // Simulasi update counter (dummy data)
            this.lineAData.counter += Math.floor(Math.random() * 5);
            this.lineBData.counter += Math.floor(Math.random() * 5);
            
            this.updateCounterElements();
        }, 5000);
    }

    // Method untuk dipanggil dari dashboard page 2
    syncWithProductionData(lineAData, lineBData) {
        if (lineAData) {
            this.lineAData.processStop = lineAData.processStopMinutes || 0;
            this.lineAData.machineStop = lineAData.machineStopMinutes || 0;
        }

        if (lineBData) {
            this.lineBData.processStop = lineBData.processStopMinutes || 0;
            this.lineBData.machineStop = lineBData.machineStopMinutes || 0;
        }

        this.updateCounterElements();
    }
}

// Export for use in other files
if (typeof module !== 'undefined' && module.exports) {
    module.exports = CounterDashboard;
}
